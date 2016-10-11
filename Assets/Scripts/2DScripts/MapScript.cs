using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapScript : MonoBehaviour
{
    public int batteryCount;
    public Text batteryText;
    private GameObject[] allBatteries;
    private List<GameObject> BatteriesHitList;
    private List<int> batteryUsedList;
    private List<int> batteryPickups;
    private List<int> batteryPickupsCount;
    private Color batteryColor;
    private Color batterySelectedColor;

    public Transform LowBatteryPrefab;
    private Transform LowBattery;

    //public Transform DynamicBatteryPrefab;
    public GameObject DynamicBattery;
    private TextMesh dynamicBatteryText;

    public Transform PlayerShadowPrefab;
    private List<Object> playerShadowPrefabList;
    private Vector3 prevShadowPos;

    public Transform LinePrefab;
    private List<Object> linePrefabList;
    private Transform lineDynamic;

    private Stack<GameObject> ammoList;
    private Stack<int> ammoPickupsCount;
    private GameObject[] allAmmos;
    private GameObject[] ammolistarray;
    private Color ammoColor;
    private Color ammoSelectedColor;

    private Texture2D cursorGreen;
    private Texture2D cursorRed;
    private Vector2 cursorGreenHotspot, cursorRedHotspot;

    public Transform CrossPrefab;
    private Transform cross;


    public Text EndText;

    public Player2D player2D;
    public List<Vector3> playerPosList;

    RaycastHit[] hitsEveryFrame;
    RaycastHit[] hits1;
    

    private List<Vector2> mapPoints;
    private List<int> distanceTravelled;

    public GameObject SoundManager;

    private LayerMask wallLayerMask;
    private LayerMask ammoLayerMask;
    private LayerMask batteryLayerMask;
    private LayerMask targetLayerMask;

    private int thresholdDistance;



    public Vector2 convertToPixels(Vector3 worldPos)
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        float unitsToPixels = sp.sprite.pixelsPerUnit;
        Vector3 sourcePixelPos = transform.InverseTransformPoint(worldPos);

        Vector2 tranFormedPos;
        tranFormedPos.x = sourcePixelPos.x * unitsToPixels;
        tranFormedPos.y = sourcePixelPos.y * unitsToPixels;

        return tranFormedPos;
    }

    void Start()
    {
        mapPoints = GameManager.Instance.mapPoints;
        mapPoints.Clear();
        Vector2 imagePos = convertToPixels(prevShadowPos);
        mapPoints.Add(imagePos);
        playerPosList = new List<Vector3>();
        playerPosList.Add(prevShadowPos);

        int currentBattery = System.Int32.Parse(batteryText.text);
        BatteriesHitList = GameManager.Instance.BatteriesHitList;
        BatteriesHitList.Clear();
        batteryUsedList = GameManager.Instance.batteryUsedList;
        batteryUsedList.Clear();
        batteryPickups = GameManager.Instance.batteryPickups;
        batteryPickups.Clear();
        batteryPickupsCount = GameManager.Instance.batteryPickupsCount;
        batteryPickupsCount.Clear();
        batteryColor = Color.white;
        batterySelectedColor = Color.green;

        LowBattery = Instantiate(LowBatteryPrefab) as Transform;
        LowBattery.gameObject.SetActive(false);

        //DynamicBattery = Instantiate(DynamicBatteryPrefab) as Transform;
        DynamicBattery.SetActive(false);
        dynamicBatteryText = DynamicBattery.GetComponent<TextMesh>();

        distanceTravelled = GameManager.Instance.distanceTravelled;
        distanceTravelled.Clear();

        playerShadowPrefabList = new List<Object>();
        Instantiate(PlayerShadowPrefab, prevShadowPos, Quaternion.identity);

        linePrefabList = new List<Object>();
        lineDynamic = Instantiate(LinePrefab, prevShadowPos, Quaternion.identity) as Transform;

        ammoList = new Stack<GameObject>();
        ammoPickupsCount = new Stack<int>();
        allAmmos = GameObject.FindGameObjectsWithTag("Ammo");
        allBatteries = GameObject.FindGameObjectsWithTag("Battery");
        wallLayerMask = 1 << 12;
        ammoLayerMask = 1 << 13;
        batteryLayerMask = 1 << 14;
        targetLayerMask = 1 << 15;
        ammoColor = Color.white;
        ammoSelectedColor = Color.yellow;

        cross = Instantiate(CrossPrefab) as Transform;
        cross.gameObject.SetActive(false);

        cursorGreen = Resources.Load("Sprites/Robot") as Texture2D;
        cursorGreenHotspot.x = cursorGreen.width / 2;
        cursorGreenHotspot.y = cursorGreen.height / 2;

        cursorRed = Resources.Load("Sprites/Robot_red") as Texture2D;
        cursorRedHotspot.x = cursorRed.width / 2;
        cursorRedHotspot.y = cursorRed.height / 2;

        GameManager.Instance.width2DPlane = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect.width;
        GameManager.Instance.height2DPlane = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect.height;

        thresholdDistance = (currentBattery / GameManager.Instance.batteryDepletionRate);
        GameManager.Instance.headShots = 0;
        GameManager.Instance.totalEnemiesKilled = 0;
    }

    public void setPlayerInitialPos(Vector3 playerInitialPos)
    {
        prevShadowPos = playerInitialPos;
    }

    private int countObjects(Vector3 mousePos, LayerMask layerMask, out RaycastHit[] hit)
    {
        Vector3 object_vector;
        float rayLength;
        object_vector = mousePos - prevShadowPos;
        rayLength = object_vector.magnitude;
        //rayLength = (thresholdDistance * thresholdDistance < object_vector.sqrMagnitude) ? thresholdDistance : object_vector.magnitude;
        hit = Physics.RaycastAll(prevShadowPos, object_vector.normalized, rayLength, layerMask);
        return hit.Length;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;

        LineRenderer LineR = lineDynamic.GetComponent<LineRenderer>();
        LineR.SetPosition(0, prevShadowPos);

        int travelDist = (int)Mathf.Ceil(Vector3.Distance(prevShadowPos, mousePos));
        int currentBattery = System.Int32.Parse(batteryText.text);
        
        

        for (int i = 0; i < allBatteries.Length; i++)                                                               //set all batteries false
        {
            allBatteries[i].GetComponent<SpriteRenderer>().color = batteryColor;
        }

        for (int i = 0; i < BatteriesHitList.Count; i++)                                                               //set true selected bettery
        {
            BatteriesHitList[i].GetComponent<SpriteRenderer>().color = batterySelectedColor;
        }

        for (int i = 0; i < allAmmos.Length; i++)                                                               //set all ammo false
        {
            allAmmos[i].GetComponent<SpriteRenderer>().color = ammoColor;
        }

        for (int i = 0; i < ammoList.Count; i++)                                                               //set true selected ammo
        {
            ammolistarray = ammoList.ToArray();
            ammolistarray[i].GetComponent<SpriteRenderer>().color = ammoSelectedColor;
        }


        if ((countObjects(mousePos, wallLayerMask, out hits1) == 0))                                        //green
        {
            Cursor.SetCursor(cursorGreen, cursorGreenHotspot, CursorMode.Auto);
            LineR.SetColors(Color.black, Color.black);
            // show red cursor if we cannot have battery to move there
            LineR.SetPosition(1, mousePos);
            LowBattery.gameObject.SetActive(false);
            DynamicBattery.SetActive(true);
            DynamicBattery.transform.position = mousePos + new Vector3(0.8f, 1, 0);
            
            cross.gameObject.SetActive(false);

            
            if ((currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate)/*  + countObjects(mousePos, batteryLayerMask, out hitsEveryFrame) * 50*/) < 0)
            {
                Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
                LineR.SetColors(Color.red, Color.red);

                LineR.SetPosition(1, (((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                LowBattery.gameObject.SetActive(true);
                LowBattery.position = ((((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                //DynamicBattery.position = ((((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                dynamicBatteryText.text = "0";
            }
            else
            {
                //battery detection
                if (countObjects(mousePos, batteryLayerMask, out hitsEveryFrame) > 0)
                {                                                                                                       //battery detection
                    for (int i = 0; i < hitsEveryFrame.Length; i++)
                    {
                        hitsEveryFrame[i].transform.gameObject.GetComponent<SpriteRenderer>().color = batterySelectedColor;
                    }
                }
                dynamicBatteryText.text = (currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) + hitsEveryFrame.Length*50).ToString();

                if (countObjects(mousePos, ammoLayerMask, out hitsEveryFrame) > 0)                                      //ammo vicinity detection
                {
                    for (int i = 0; i < hitsEveryFrame.Length; i++)
                    {
                        hitsEveryFrame[i].transform.GetComponent<SpriteRenderer>().color = ammoSelectedColor;
                    }
                }
                
            }
        }
        else                                                                                        //red cuz of wall
        {
            Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
            LineR.SetColors(Color.red, Color.red);
            LowBattery.gameObject.SetActive(false);
            cross.gameObject.SetActive(true);
            DynamicBattery.SetActive(false);
            RaycastHit hit;
            Physics.Raycast(prevShadowPos, (mousePos - prevShadowPos).normalized, out hit, (mousePos - prevShadowPos).magnitude, wallLayerMask);
            LineR.SetPosition(1, hit.point);
            cross.position = hit.point;

            if ((currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) /*+ countObjects(mousePos, batteryLayerMask, out hitsEveryFrame) * 50*/) < 0)
            {
                if ((hit.point - prevShadowPos).magnitude > thresholdDistance)
                {
                    cross.gameObject.SetActive(false);
                    LowBattery.gameObject.SetActive(true);
                    LowBattery.position = ((((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                    LineR.SetPosition(1, (((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoPrevMove();
        }
    }

    void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0.0f;

        RaycastHit[] hits;

        if (countObjects(worldPos, wallLayerMask, out hits) == 0)
        {
            int travelDist = (int)Mathf.Ceil(Vector3.Distance(prevShadowPos, worldPos));
            distanceTravelled.Add(travelDist);
            int currentBattery = System.Int32.Parse(batteryText.text);
            if ((currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) /*+ countObjects(worldPos, batteryLayerMask, out hitsEveryFrame) * 50*/) >= 0)
            {
                SoundManager.GetComponent<Audio>().MouseClicked();
                int batteryLeft = currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate);
                int batteriesPicked = 0;
                if (countObjects(worldPos, batteryLayerMask, out hits) > 0)
                {                                                                                                       //battery detection
                    for (int i = 0; i < hits.Length; i++)
                    {
                        BatteriesHitList.Add(hits[i].transform.gameObject);
                        hits[i].transform.gameObject.GetComponent<SpriteRenderer>().color = batterySelectedColor;
                        hits[i].collider.enabled = false;
                        batteryPickups.Add(50);
                        batteryLeft += 50;
                        batteriesPicked++;
                    }
                }
                batteryPickupsCount.Add(batteriesPicked);
                batteryUsedList.Add((travelDist * GameManager.Instance.batteryDepletionRate));
                batteryText.text = batteryLeft.ToString();

                if (countObjects(worldPos, ammoLayerMask, out hits) > 0)                                      //ammo vicinity detection
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        ammoList.Push(hits[i].transform.gameObject);
                        hits[i].transform.GetComponent<SpriteRenderer>().color = ammoSelectedColor;
                        hits[i].collider.enabled = false;
                    }
                }
                ammoPickupsCount.Push(hits.Length);

                if (countObjects(worldPos, targetLayerMask, out hits) > 0)                                      //Target Detection
                {
                    EndText.gameObject.SetActive(true);
                }

                // draw the line and shadow
                Vector2 imagePos = convertToPixels(worldPos);
                mapPoints.Add(imagePos);

                playerPosList.Add(worldPos);
                Object playerShadowprefab = Instantiate(PlayerShadowPrefab, worldPos, Quaternion.identity);
                playerShadowPrefabList.Add(playerShadowprefab);

                Transform line = Instantiate(LinePrefab, prevShadowPos, Quaternion.identity) as Transform;
                linePrefabList.Add(line);
                LineRenderer LineR = line.GetComponent<LineRenderer>();
                LineR.SetPosition(0, prevShadowPos);
                LineR.SetPosition(1, worldPos);

                prevShadowPos = worldPos;
                currentBattery = System.Int32.Parse(batteryText.text);
                thresholdDistance = (currentBattery / GameManager.Instance.batteryDepletionRate);
            }
            else
            {
                SoundManager.GetComponent<Audio>().WrongClick();
            }
        }
        else
        {
            SoundManager.GetComponent<Audio>().WrongClick();
        }
    }


    private void UndoPrevMove()
    {
        EndText.gameObject.SetActive(false);
        if (playerShadowPrefabList.Count > 0)
        {
            SoundManager.GetComponent<Audio>().Undo();
            mapPoints.RemoveAt(mapPoints.Count - 1);

            Transform prevShadow = playerShadowPrefabList[playerShadowPrefabList.Count - 1] as Transform;
            Destroy(prevShadow.gameObject);
            playerShadowPrefabList.RemoveAt(playerShadowPrefabList.Count - 1);

            UndoLine();

            UndoBattery();

            UndoAmmo();

            playerPosList.RemoveAt(playerPosList.Count - 1);
            prevShadowPos = playerPosList[playerPosList.Count - 1];

            if (player2D.currentPosIndex > playerPosList.Count - 1)
            {
                player2D.currentPosIndex = playerPosList.Count - 1;
                player2D.gameObject.transform.position = playerPosList[playerPosList.Count - 1];
                player2D.source = player2D.gameObject.transform.position;
                player2D.destination = player2D.gameObject.transform.position;
            }

            distanceTravelled.RemoveAt(distanceTravelled.Count - 1);
        }
    }

    private void UndoBattery()
    {
        int batteriesToRemove = batteryPickupsCount[batteryPickupsCount.Count - 1];
        for (int i = 0; i < batteriesToRemove; i++)
        {
            BatteriesHitList[BatteriesHitList.Count - 1].GetComponent<SpriteRenderer>().color = Color.white;
            BatteriesHitList[BatteriesHitList.Count - 1].GetComponent<Collider>().enabled = true;
            BatteriesHitList.RemoveAt(BatteriesHitList.Count - 1);
        }

        int batteryAmtToRemove = batteriesToRemove * 50;
        batteryPickups.RemoveRange(batteryPickups.Count - batteriesToRemove, batteriesToRemove);   // remove the corresponding number of batteries
        batteryPickupsCount.RemoveAt(batteryPickupsCount.Count - 1);                               //remove the last element of batteryPickupsCount list

        // add back the battery used
        int batteryUsed = batteryUsedList[batteryUsedList.Count - 1];
        batteryUsedList.RemoveAt(batteryUsedList.Count - 1);

        int currentBattery = System.Int32.Parse(batteryText.text);
        int batteryLeft = currentBattery + batteryUsed - batteryAmtToRemove;
        batteryText.text = batteryLeft.ToString();

        currentBattery = System.Int32.Parse(batteryText.text);
        thresholdDistance = (currentBattery / GameManager.Instance.batteryDepletionRate);
    }

    private void UndoAmmo()
    {
        if (ammoList.Count > 0)
        {
            int ammosToRemove = ammoPickupsCount.Pop();
            for (int i = 0; i < ammosToRemove; i++)
            {
                GameObject ammoPoped = ammoList.Pop();
                ammoPoped.GetComponent<SpriteRenderer>().color = Color.white;
                ammoPoped.GetComponent<Collider>().enabled = true;
            }
        }
    }

    private void UndoLine()
    {
        Transform prevLine = linePrefabList[linePrefabList.Count - 1] as Transform;
        Destroy(prevLine.gameObject);
        linePrefabList.RemoveAt(linePrefabList.Count - 1);
    }
}
