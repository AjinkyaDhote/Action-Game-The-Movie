using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{
    public int batteryCount;
    public Text batteryText;
    private GameObject[] allBatteries;
    private Stack<GameObject> BatteriesHitList;
    private GameObject[] BatteriesHitListArray;
    private List<int> batteryUsedList;
    private List<int> batteryPickups;
    private List<int> batteryPickupsCount;
    private Color batteryColor;
    private Color batterySelectedColor;
    private bool batteryDetected;

    public Transform LowBatteryPrefab;
    private Transform LowBattery;

    //public Transform DynamicBatteryPrefab;
    public GameObject DynamicBattery;
    private TextMesh dynamicBatteryText;
    private SpriteRenderer dynamicBatterySprite;

    public Transform PlayerShadowPrefab;
    private Stack<Object> playerShadowPrefabList;
    private Vector3 prevShadowPos;

    public Transform LinePrefab;
    private Stack<Object> linePrefabList;
    private Transform lineDynamic;

    private Stack<GameObject> ammoList;
    private Stack<int> ammoPickupsCount;
    private GameObject[] allAmmos;
    private GameObject[] ammolistarray;
    private Color ammoColor;
    private Color ammoSelectedColor;
    private bool ammoDetected;

    private Stack<GameObject> lockList;
    private Stack<int> lockPickupsCount;
    private GameObject[] allLocks;
    private GameObject[] locklistarray;
    private Color lockColor;
    private Color lockSelectedColorUnlocked;
    private Color lockSelectedColorLocked;
    private bool lockDetected;

    private Stack<GameObject> keyList;
    private Stack<int> keyPickupsCount;
    private GameObject[] allKeys;
    private GameObject[] keylistarray;
    private Color keyColor;
    private Color keySelectedColor;
    private bool keyDetected;

    private Texture2D cursorGreen;
    private Texture2D cursorRed;
    private Vector2 cursorGreenHotspot, cursorRedHotspot;

    public Transform CrossPrefab;
    private Transform cross;

    public GameObject Target;
    private SpriteRenderer targetSprite;
    public Text EndText;
    bool targetReached;
    bool targetDetected;
    private Color targetColor;
    private Color targetSelectedColor;

    public Player2D player2D;
    public List<Vector3> playerPosList;

    RaycastHit2D[] hitsEveryFrame;
    RaycastHit2D[] hits1;


    private List<Vector2> mapPoints;
    private List<int> distanceTravelled;

    public Audio SoundManager;

    private LayerMask wallLayerMask;
    private LayerMask ammoLayerMask;
    private LayerMask lockLayerMask;
    private LayerMask keyLayerMask;
    private LayerMask batteryLayerMask;
    private LayerMask targetLayerMask;
    private LayerMask pickupLayerMask;

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

        if (SceneManager.GetActiveScene().buildIndex == 7)
            batteryText.text = "50";
        else
            batteryText.text = "100";
        int currentBattery = System.Int32.Parse(batteryText.text);
        BatteriesHitList = GameManager.Instance.BatteriesHitList;
        BatteriesHitList.Clear();
        batteryUsedList = GameManager.Instance.batteryUsedList;
        batteryUsedList.Clear();
        batteryPickups = GameManager.Instance.batteryPickups;
        batteryPickups.Clear();
        batteryPickupsCount = GameManager.Instance.batteryPickupsCount;
        batteryPickupsCount.Clear();
        batteryColor = new Color(1, 1, 1, 0.25f);//Color.white;
        batterySelectedColor = new Color(1, 1, 1, 1f);//Color.blue;
        batteryDetected = false;

        LowBattery = Instantiate(LowBatteryPrefab) as Transform;
        LowBattery.gameObject.SetActive(false);

        //DynamicBattery = Instantiate(DynamicBatteryPrefab) as Transform;
        DynamicBattery.SetActive(false);
        dynamicBatteryText = DynamicBattery.GetComponent<TextMesh>();
        dynamicBatterySprite = DynamicBattery.transform.GetChild(0).GetComponent<SpriteRenderer>();

        distanceTravelled = GameManager.Instance.distanceTravelled;
        distanceTravelled.Clear();

        targetSprite = Target.GetComponent<SpriteRenderer>();
        targetReached = false;
        targetColor = Color.white;
        targetSelectedColor = Color.cyan;

        playerShadowPrefabList = new Stack<Object>();
        Instantiate(PlayerShadowPrefab, prevShadowPos, Quaternion.identity);

        linePrefabList = new Stack<Object>();
        lineDynamic = Instantiate(LinePrefab, prevShadowPos, Quaternion.identity) as Transform;

        ammoList = new Stack<GameObject>();
        ammoPickupsCount = new Stack<int>();
        allAmmos = GameObject.FindGameObjectsWithTag("Ammo");
        lockList = new Stack<GameObject>();
        lockPickupsCount = new Stack<int>();
        allLocks = GameObject.FindGameObjectsWithTag("Lock");
        keyList = new Stack<GameObject>();
        keyPickupsCount = new Stack<int>();
        allKeys = GameObject.FindGameObjectsWithTag("Key");
        allBatteries = GameObject.FindGameObjectsWithTag("Battery");
        wallLayerMask = 1 << 12;
        ammoLayerMask = 1 << 13;
        lockLayerMask = 1 << 20;
        keyLayerMask = 1 << 21;
        batteryLayerMask = 1 << 14;
        targetLayerMask = 1 << 15;
        pickupLayerMask = ammoLayerMask | batteryLayerMask | targetLayerMask | lockLayerMask | keyLayerMask;
        ammoColor = new Color(1, 1, 1, 0.25f); //Color.white;
        ammoSelectedColor = new Color(1, 1, 1, 1f);//Color.blue;
        ammoDetected = false;
        lockColor = new Color(1, 1, 1, 0.25f); //Color.white;
        lockSelectedColorUnlocked = new Color(1, 1, 1, 1f);//Color.blue;
        lockSelectedColorLocked = new Color(1, 0.5f, 0.5f, 1f);
        lockDetected = false;
        keyColor = new Color(1, 1, 1, 0.25f); //Color.white;
        keySelectedColor = new Color(1, 1, 1, 1f);//Color.blue;
        keyDetected = false;
        targetDetected = false;
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
        GameManager.Instance.totalDistance = 0;

        GameManager.Instance.playAvailable = false;
    }

    public void setPlayerInitialPos(Vector3 playerInitialPos)
    {
        prevShadowPos = playerInitialPos;
    }

    private int countObjects(Vector3 mousePos, LayerMask layerMask, out RaycastHit2D[] hit)
    {
        Vector3 object_vector;
        float rayLength;
        object_vector = mousePos - prevShadowPos;
        //rayLength = object_vector.magnitude;
        rayLength = (thresholdDistance * thresholdDistance < object_vector.sqrMagnitude) ? thresholdDistance : object_vector.magnitude;
        RaycastHit2D[] hit1;
        hit1 = Physics2D.RaycastAll(prevShadowPos, object_vector.normalized, rayLength, layerMask);
        //foreach (RaycastHit2D h in hit1)
        //    Debug.Log("Hit " + h.transform.name);
        if (layerMask == ammoLayerMask)
        {
            for (int i = 0; i < hit1.Length; i++)
            {
                if (Physics2D.Raycast(hit1[i].point, (Vector2)hit1[i].transform.position - hit1[i].point, ((Vector2)hit1[i].transform.position - hit1[i].point).magnitude, wallLayerMask))
                {
                    Debug.DrawRay(hit1[i].point, (Vector2)hit1[i].transform.position - hit1[i].point, Color.black);
                    hit1 = RemoveElements(hit1, i);
                }
            }
            //foreach (RaycastHit2D h in hit1)
            //    Debug.Log(h.transform.name);
        }
        hit = hit1;
        return hit.Length;
    }

    private RaycastHit2D[] RemoveElements(RaycastHit2D[] ElementsArray, int RemoveAt)
    {
        RaycastHit2D[] newElementsArray = new RaycastHit2D[ElementsArray.Length - 1];

        int i = 0;
        int j = 0;
        while (i < ElementsArray.Length)
        {
            if (i != RemoveAt)
            {
                newElementsArray[j] = ElementsArray[i];
                j++;
            }
            i++;
        }
        return newElementsArray;
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

        BatteriesHitListArray = BatteriesHitList.ToArray();
        for (int i = 0; i < BatteriesHitList.Count; i++)                                                               //set true selected bettery
        {
            BatteriesHitListArray[i].GetComponent<SpriteRenderer>().color = batterySelectedColor;
        }

        for (int i = 0; i < allAmmos.Length; i++)                                                               //set all ammo false
        {
            allAmmos[i].GetComponent<SpriteRenderer>().color = ammoColor;
        }

        ammolistarray = ammoList.ToArray();
        for (int i = 0; i < ammoList.Count; i++)                                                               //set true selected ammo
        {
            ammolistarray[i].GetComponent<SpriteRenderer>().color = ammoSelectedColor;
        }

        for (int i = 0; i < allLocks.Length; i++)                                                               //set all lock false
        {
            allLocks[i].GetComponent<SpriteRenderer>().color = lockColor;
        }

        locklistarray = lockList.ToArray();
        for (int i = 0; i < lockList.Count; i++)                                                               //set true selected lock
        {
            locklistarray[i].GetComponent<SpriteRenderer>().color = lockSelectedColorUnlocked;
        }

        for (int i = 0; i < allKeys.Length; i++)                                                               //set all key false
        {
            allKeys[i].GetComponent<SpriteRenderer>().color = keyColor;
        }

        keylistarray = keyList.ToArray();
        for (int i = 0; i < keyList.Count; i++)                                                               //set true selected key
        {
            keylistarray[i].GetComponent<SpriteRenderer>().color = keySelectedColor;
        }

        if (!targetReached)
            targetSprite.color = targetColor;

        if ((countObjects(mousePos, wallLayerMask, out hits1) == 0))                                        //green
        {
            Cursor.SetCursor(cursorGreen, cursorGreenHotspot, CursorMode.Auto);
            LineR.SetColors(Color.black, Color.black);
            // show red cursor if we cannot have battery to move there
            LineR.SetPosition(1, mousePos);
            LowBattery.gameObject.SetActive(false);
            DynamicBattery.SetActive(true);
            DynamicBattery.transform.position = mousePos + new Vector3(0.8f, 1, 0);
            dynamicBatteryText.color = Color.black;
            dynamicBatterySprite.color = Color.black;

            cross.gameObject.SetActive(false);

            //update threshold distance dynamically according to batteries in line
            thresholdDistance = ((currentBattery + countObjects(mousePos, batteryLayerMask, out hitsEveryFrame) * 50) / GameManager.Instance.batteryDepletionRate);

            if ((currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) + countObjects(mousePos, batteryLayerMask, out hitsEveryFrame) * 50) < 0)
            {
                Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
                LineR.SetColors(Color.red, Color.red);

                LineR.SetPosition(1, (((mousePos - prevShadowPos).normalized) * (thresholdDistance)) + prevShadowPos);
                LowBattery.gameObject.SetActive(true);
                LowBattery.position = ((((mousePos - prevShadowPos).normalized) * (thresholdDistance)) + prevShadowPos);
                //DynamicBattery.position = ((((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                dynamicBatteryText.text = "0";
                dynamicBatteryText.color = Color.red;
                dynamicBatterySprite.color = Color.red;
            }
            else
            {
                //battery detection
                if (countObjects(mousePos, batteryLayerMask, out hitsEveryFrame) > 0)                                               //battery detection
                {
                    if (!batteryDetected)
                    {
                        batteryDetected = true;
                        SoundManager.PickupHover();
                    }
                    for (int i = 0; i < hitsEveryFrame.Length; i++)
                    {
                        hitsEveryFrame[i].transform.gameObject.GetComponent<SpriteRenderer>().color = batterySelectedColor;
                    }
                }
                else
                    batteryDetected = false;

                dynamicBatteryText.text = (currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) + hitsEveryFrame.Length * 50).ToString();

                if (countObjects(mousePos, ammoLayerMask, out hitsEveryFrame) > 0)                                      //ammo vicinity detection
                {
                    if (!ammoDetected)
                    {
                        ammoDetected = true;
                        SoundManager.PickupHover();
                    }
                    for (int i = 0; i < hitsEveryFrame.Length; i++)
                    {
                        hitsEveryFrame[i].transform.GetComponent<SpriteRenderer>().color = ammoSelectedColor;
                        //hitsEveryFrame[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                else
                    ammoDetected = false;

                if (countObjects(mousePos, lockLayerMask, out hitsEveryFrame) > 0)                                      //lock vicinity detection
                {
                    if (!lockDetected)
                    {
                        lockDetected = true;
                        SoundManager.PickupHover();
                    }
                    for (int i = 0; i < hitsEveryFrame.Length; i++)
                    {
                        hitsEveryFrame[i].transform.GetComponent<SpriteRenderer>().color = lockSelectedColorUnlocked;
                    }
                }
                else
                    lockDetected = false;

                if (countObjects(mousePos, keyLayerMask, out hitsEveryFrame) > 0)                                      //key vicinity detection
                {
                    if (!keyDetected)
                    {
                        keyDetected = true;
                        SoundManager.PickupHover();
                    }
                    for (int i = 0; i < hitsEveryFrame.Length; i++)
                    {
                        hitsEveryFrame[i].transform.GetComponent<SpriteRenderer>().color = keySelectedColor;
                    }
                }
                else
                    keyDetected = false;

                if (countObjects(mousePos, targetLayerMask, out hitsEveryFrame) > 0)                                              //Target Detection
                {
                    if (!targetDetected)
                    {
                        targetDetected = true;
                        SoundManager.TargetHover();
                    }
                    targetSprite.color = targetSelectedColor;
                }
                else
                    targetDetected = false;

            }
        }
        else                                                                                        //red cuz of wall
        {
            Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
            LineR.SetColors(Color.red, Color.red);
            LowBattery.gameObject.SetActive(false);
            cross.gameObject.SetActive(true);
            DynamicBattery.SetActive(false);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(prevShadowPos, (mousePos - prevShadowPos).normalized, (mousePos - prevShadowPos).magnitude, wallLayerMask);
            LineR.SetPosition(1, hit.point);
            cross.position = hit.point;
        }

        if (Input.GetMouseButtonDown(1))
        {
            UndoPrevMove();
        }
    }

    void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0.0f;

        RaycastHit2D[] hits;

        if (countObjects(worldPos, wallLayerMask, out hits) == 0)
        {
            int travelDist = (int)Mathf.Ceil(Vector3.Distance(prevShadowPos, worldPos));
            distanceTravelled.Add(travelDist);
            int currentBattery = System.Int32.Parse(batteryText.text);
            if ((currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) + countObjects(worldPos, batteryLayerMask, out hitsEveryFrame) * 50) >= 0)
            {
                SoundManager.MouseClicked();
                int batteryLeft = currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate);
                int batteriesPicked = 0;
                if (countObjects(worldPos, batteryLayerMask, out hits) > 0)
                {                                                                                                       //battery detection
                    SoundManager.BatteryPickup();
                    for (int i = 0; i < hits.Length; i++)
                    {
                        BatteriesHitList.Push(hits[i].transform.gameObject);
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

                if (countObjects(worldPos, ammoLayerMask, out hits) > 0)                                                //ammo vicinity detection
                {
                    SoundManager.AmmoPickup();
                    for (int i = 0; i < hits.Length; i++)
                    {
                        ammoList.Push(hits[i].transform.gameObject);
                        hits[i].transform.GetComponent<SpriteRenderer>().color = ammoSelectedColor;
                        hits[i].collider.enabled = false;
                    }
                }
                ammoPickupsCount.Push(hits.Length);

                if (countObjects(worldPos, lockLayerMask, out hits) > 0)                                                //lock vicinity detection
                {
                    //SoundManager.LockPickup();
                    for (int i = 0; i < hits.Length; i++)
                    {
                        lockList.Push(hits[i].transform.gameObject);
                        hits[i].transform.GetComponent<SpriteRenderer>().color = lockSelectedColorUnlocked;
                        hits[i].collider.enabled = false;
                    }
                }
                lockPickupsCount.Push(hits.Length);

                if (countObjects(worldPos, keyLayerMask, out hits) > 0)                                                //key vicinity detection
                {
                    //SoundManager.KeyPickup();
                    for (int i = 0; i < hits.Length; i++)
                    {
                        keyList.Push(hits[i].transform.gameObject);
                        hits[i].transform.GetComponent<SpriteRenderer>().color = keySelectedColor;
                        hits[i].collider.enabled = false;
                    }
                }
                keyPickupsCount.Push(hits.Length);

                if (countObjects(worldPos, targetLayerMask, out hits) > 0)                                              //Target Detection
                {
                    SoundManager.TargetReached();
                    targetSprite.color = targetSelectedColor;
                    EndText.gameObject.SetActive(true);
                    GameManager.Instance.playAvailable = true;
                    targetReached = true;
                }

                //if(countObjects(worldPos, pickupLayerMask, out hits) == 0)
                //{

                //}

                // draw the line and shadow
                Vector2 imagePos = convertToPixels(worldPos);
                mapPoints.Add(imagePos);

                playerPosList.Add(worldPos);
                Object playerShadowprefab = Instantiate(PlayerShadowPrefab, worldPos, Quaternion.identity);
                playerShadowPrefabList.Push(playerShadowprefab);

                Transform line = Instantiate(LinePrefab, prevShadowPos, Quaternion.identity) as Transform;
                linePrefabList.Push(line);
                LineRenderer LineR = line.GetComponent<LineRenderer>();
                LineR.SetPosition(0, prevShadowPos);
                LineR.SetPosition(1, worldPos);

                prevShadowPos = worldPos;
                currentBattery = System.Int32.Parse(batteryText.text);
                thresholdDistance = (currentBattery / GameManager.Instance.batteryDepletionRate);
            }
            else
            {
                SoundManager.WrongClick();
            }
        }
        else
        {
            SoundManager.WrongClick();
        }
    }


    private void UndoPrevMove()
    {
        {
            targetSprite.color = targetColor;
            EndText.gameObject.SetActive(false);
            targetReached = false;
            GameManager.Instance.playAvailable = false;
        }

        if (playerShadowPrefabList.Count > 0)
        {
            SoundManager.Undo();
            mapPoints.RemoveAt(mapPoints.Count - 1);

            Transform prevShadow = playerShadowPrefabList.Pop() as Transform;
            Destroy(prevShadow.gameObject);

            UndoLine();

            UndoBattery();

            UndoAmmo();

            UndoLock();

            UndoKey();

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
            GameObject batteryPoped = BatteriesHitList.Pop();
            batteryPoped.GetComponent<SpriteRenderer>().color = Color.white;
            batteryPoped.GetComponent<Collider2D>().enabled = true;
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
                ammoPoped.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void UndoLock()
    {
        if (lockList.Count > 0)
        {
            int locksToRemove = lockPickupsCount.Pop();
            for (int i = 0; i < locksToRemove; i++)
            {
                GameObject lockPoped = lockList.Pop();
                lockPoped.GetComponent<SpriteRenderer>().color = Color.white;
                lockPoped.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void UndoKey()
    {
        if (keyList.Count > 0)
        {
            int keysToRemove = keyPickupsCount.Pop();
            for (int i = 0; i < keysToRemove; i++)
            {
                GameObject keyPoped = keyList.Pop();
                keyPoped.GetComponent<SpriteRenderer>().color = Color.white;
                keyPoped.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void UndoLine()
    {
        Transform prevLine = linePrefabList.Pop() as Transform;
        Destroy(prevLine.gameObject);
    }
}
