using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapScript : MonoBehaviour
{
    public Transform PlayerShadowPrefab;
    public Transform LinePrefab;
    //public Transform LinePrefabDynamic;
    public Texture2D cursorGreen;
    public Texture2D cursorRed;
    public Player2D player2D;
    public List<Vector3> playerPosList;
    public Text batteryText;
    public RaycastHit[] hits;

    private RaycastHit hit;
    private List<Vector2> mapPoints;
    private Vector3 prevShadowPos;
    private Vector2 cursorGreenHotspot, cursorRedHotspot;
    private List<Object> playerShadowPrefabList;
    private List<Object> linePrefabList;
    private List<GameObject> BatteriesHitList; private List<GameObject> ammosHitList;
    private List<int> batteryUsedList;
    private List<int> batteryPickups; private List<int> ammoPickups;
    private List<int> batteryPickupsCount; private List<int> ammoPickupsCount;
    private Transform lineDynamic;
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

        //BatteryPos = GameManager.Instance.BatteryPos;
        //BatteryPos.Clear();
        BatteriesHitList = GameManager.Instance.BatteriesHitList;
        BatteriesHitList.Clear();

        batteryUsedList = GameManager.Instance.batteryUsedList;
        batteryUsedList.Clear();
        //batteryUsedList = new List<int>();

        batteryPickups = GameManager.Instance.batteryPickups;
        batteryPickups.Clear();

        batteryPickupsCount = GameManager.Instance.batteryPickupsCount;
        batteryPickupsCount.Clear();

        ammoPickups = GameManager.Instance.ammoPickups;
        ammoPickups.Clear();

        ammoPickupsCount = GameManager.Instance.ammoPickupsCount;
        ammoPickupsCount.Clear();

        ammosHitList = GameManager.Instance.ammosHitList;
        ammosHitList.Clear();

        playerShadowPrefabList = new List<Object>();
        linePrefabList = new List<Object>();
        playerPosList = new List<Vector3>();




        Vector2 imagePos = convertToPixels(prevShadowPos);
        mapPoints.Add(imagePos);

        //BatteryPos.Add(new Vector3(4, 1, 0));
        // BatteryPos.Add(new Vector3(4, 5, 0));
        // BatteryPos.Add(new Vector3(5, 1, 0));

        /*
            GameObject Bat1 = (GameObject)Instantiate(BatteryPrefab, BatteryPos[0], Quaternion.identity);
            if (Bat1 != null)
            {
                Debug.Log("Hello");
                Bat1.transform.SetParent(BatteryRoot);
            }

           */

        playerPosList.Add(prevShadowPos);
        Instantiate(PlayerShadowPrefab, prevShadowPos, Quaternion.identity);

        lineDynamic = Instantiate(LinePrefab, prevShadowPos, Quaternion.identity) as Transform;

        cursorGreenHotspot.x = cursorGreen.width / 2;
        cursorGreenHotspot.y = cursorGreen.height / 2;

        cursorRedHotspot.x = cursorRed.width / 2;
        cursorRedHotspot.y = cursorRed.height / 2;

        GameManager.Instance.width2DPlane = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect.width;
        GameManager.Instance.height2DPlane = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect.height;

    }

    public void setPlayerInitialPos(Vector3 playerInitialPos)
    {
        prevShadowPos = playerInitialPos;
    }

    private int countObjects(Vector3 mousePos, string objectname)
    {
        Vector3 object_vector;
        int i;
        int count = 0;
        object_vector = mousePos - prevShadowPos;
        hits = Physics.RaycastAll(prevShadowPos, object_vector.normalized, object_vector.magnitude);

        //Debug.Log(hits.Length);
        for (i = 0; i < hits.Length; i++)
        {
            RaycastHit hitobject = hits[i];
            //Debug.Log(hitobject.transform.name);
            if (hitobject.transform.parent.name == objectname)
            {
                count++;
            }
        }
        return count;
        //return Physics.Raycast(prevShadowPos, object_vector.normalized, out hit, object_vector.magnitude);
    }


    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;
        //...........................

        LineRenderer LineR = lineDynamic.GetComponent<LineRenderer>();
        LineR.SetPosition(0, prevShadowPos);
        
        
        //.....................
        if (countObjects(mousePos, "wallColliders") > 0)//if (checkForObstruction(mousePos))//if(checkForObject(mousePos) && (hit.transform.parent.name == "wallColliders"))//
        {
            Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
            LineR.SetColors(Color.red, Color.red);
            /*for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.parent.name == "wallColliders")
                {
                    hit = hits[i];
                    break;
                }
            }*/
            Physics.Raycast(prevShadowPos, (mousePos - prevShadowPos).normalized, out hit);
            LineR.SetPosition(1, hit.point);
            Debug.Log(hit.transform.name);
        }
        else
        {
            Cursor.SetCursor(cursorGreen, cursorGreenHotspot, CursorMode.Auto);
            LineR.SetColors(Color.black, Color.black);
            // show red cursor if we cannot have battery to move there
            int travelDist = (int)Mathf.Ceil(Vector3.Distance(prevShadowPos, mousePos));
            int currentBattery = System.Int32.Parse(batteryText.text);

            if (currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) < 0)
            {
                Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
                LineR.SetColors(Color.red, Color.red);
            }
            LineR.SetPosition(1, mousePos);
            Debug.Log(mousePos);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoPrevMove();
        }
    }

    private void UndoPrevMove()
    {
        if (playerShadowPrefabList.Count > 0)
        {
            mapPoints.RemoveAt(mapPoints.Count - 1);

            Transform prevShadow = playerShadowPrefabList[playerShadowPrefabList.Count - 1] as Transform;
            Destroy(prevShadow.gameObject);
            playerShadowPrefabList.RemoveAt(playerShadowPrefabList.Count - 1);

            Transform prevLine = linePrefabList[linePrefabList.Count - 1] as Transform;
            Destroy(prevLine.gameObject);
            linePrefabList.RemoveAt(linePrefabList.Count - 1);

            //--------------------------------------------------------

            int batteriesToRemove = batteryPickupsCount[batteryPickupsCount.Count - 1];
            for (int i = 0; i < batteriesToRemove; i++)
            {
                BatteriesHitList[BatteriesHitList.Count - 1].SetActive(true);
                BatteriesHitList.RemoveAt(BatteriesHitList.Count - 1);
            }

            int batteryAmtToRemove = batteriesToRemove * 50;
            batteryPickups.RemoveRange(batteryPickups.Count - batteriesToRemove, batteriesToRemove);   // remove the corresponding number of batteries
            batteryPickupsCount.RemoveAt(batteryPickupsCount.Count - 1);                               //remove the last element of batteryPickupsCount list

            //Debug.Log("batteryCount");
            //for (int i = 0; i < batteryPickupsCount.Count; i++)
            //{
            //    Debug.Log(batteryPickupsCount[i]);
            //}

            //Debug.Log("Batterypickups");
            //for (int i = 0; i < batteryPickups.Count; i++)
            //{
            //    Debug.Log(batteryPickups[i]);
            //}

            // add back the battery used
            int batteryUsed = batteryUsedList[batteryUsedList.Count - 1];
            batteryUsedList.RemoveAt(batteryUsedList.Count - 1);

            int currentBattery = System.Int32.Parse(batteryText.text);
            int batteryLeft = currentBattery + batteryUsed - batteryAmtToRemove;
            batteryText.text = batteryLeft.ToString();

            //----------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------

            int ammosToRemove = ammoPickupsCount[ammoPickupsCount.Count - 1];
            for (int i = 0; i < ammosToRemove; i++)
            {
                ammosHitList[ammosHitList.Count - 1].SetActive(true);
                ammosHitList.RemoveAt(ammosHitList.Count - 1);
            }

            //int ammoAmtToRemove = ammosToRemove * 50;
            ammoPickups.RemoveRange(ammoPickups.Count - ammosToRemove, ammosToRemove);   // remove the corresponding number of ammos
            ammoPickupsCount.RemoveAt(ammoPickupsCount.Count - 1);                               //remove the last element of ammoPickupsCount list

            //Debug.Log("ammoCount");
            //for (int i = 0; i < ammoPickupsCount.Count; i++)
            //{
            //    Debug.Log(ammoPickupsCount[i]);
            //}

            //Debug.Log("ammoPickups");
            //for (int i = 0; i < ammoPickups.Count; i++)
            //{
            //    Debug.Log(ammoPickups[i]);
            //}
            //----------------------------------------------------------------------------------------------

            playerPosList.RemoveAt(playerPosList.Count - 1);
            prevShadowPos = playerPosList[playerPosList.Count - 1];

            if (player2D.currentPosIndex > playerPosList.Count - 1)
            {
                player2D.currentPosIndex = playerPosList.Count - 1;
                player2D.gameObject.transform.position = playerPosList[playerPosList.Count - 1];
                player2D.source = player2D.gameObject.transform.position;
                player2D.destination = player2D.gameObject.transform.position;
            }
        }
    }

    void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0.0f;

        //Debug.Log(countObjects(worldPos, "wallColliders"));

        if (countObjects(worldPos, "wallColliders") == 0)//if(!checkForWall(worldPos))//if (!checkForObject(worldPos)  || (hit.transform.parent.name != "wallColliders"))//if (!checkForObstruction(worldPos))
        {
            // reduce batteryzz
            int travelDist = (int)Mathf.Ceil(Vector3.Distance(prevShadowPos, worldPos));
            int currentBattery = System.Int32.Parse(batteryText.text);

            if (currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) >= 0)
            {
                int batteryLeft = currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate);
                int batteriesPicked = 0;
                if (countObjects(worldPos, "Batteries") > 0)                                         //battery detection
                {
                    //Debug.Log(countObjects(worldPos, "Batteries"));
                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (hits[i].transform.parent.name == "Batteries")
                        {
                            //Debug.Log(i);
                            //Destroy(hits[i].transform.gameObject);
                            BatteriesHitList.Add(hits[i].transform.gameObject);
                            hits[i].transform.gameObject.SetActive(false);
                            batteryPickups.Add(50);
                            batteryLeft += 50;
                            batteriesPicked++;
                        }
                    }

                    /*  if (batteryLeft > 100)
                      {
                          batteryLeft = batteryLeft - (batteryLeft % 100);
                      }*/
                }
                batteryPickupsCount.Add(batteriesPicked);
                //Debug.Log("batteryCount");
                //for (int i = 0; i < batteryPickupsCount.Count; i++)
                //{
                //    Debug.Log(batteryPickupsCount[i]);
                //}

                //Debug.Log("Batterypickups");
                //for (int i = 0; i < batteryPickups.Count; i++)
                //{
                //    Debug.Log(batteryPickups[i]);
                //}
                batteryUsedList.Add((travelDist * GameManager.Instance.batteryDepletionRate));
                batteryText.text = batteryLeft.ToString();

                int ammosPicked = 0;
                if (countObjects(worldPos, "Ammos") > 0)                                         //Ammo detection
                {
                    //Debug.Log(countObjects(worldPos, "Batteries"));
                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (hits[i].transform.parent.name == "Ammos")
                        {
                            //Debug.Log(i);
                            //Destroy(hits[i].transform.gameObject);
                            ammosHitList.Add(hits[i].transform.gameObject);
                            hits[i].transform.gameObject.SetActive(false);
                            ammoPickups.Add(10);
                            //batteryLeft += 10;
                            ammosPicked++;
                        }
                    }

                    /*  if (batteryLeft > 100)
                      {
                          batteryLeft = batteryLeft - (batteryLeft % 100);
                      }*/
                }
                ammoPickupsCount.Add(ammosPicked);
                //Debug.Log("ammoCount");
                //for (int i = 0; i < ammoPickupsCount.Count; i++)
                //{
                //    Debug.Log(ammoPickupsCount[i]);
                //}

                //Debug.Log("ammoPickups");
                //for (int i = 0; i < ammoPickups.Count; i++)
                //{
                //    Debug.Log(ammoPickups[i]);
                //}


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
            }
        }
    }
}
