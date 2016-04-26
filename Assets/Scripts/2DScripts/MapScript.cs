using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapScript : MonoBehaviour
{
    public Transform PlayerShadowPrefab;
    public Transform LinePrefab;
    //public Transform LinePrefabDynamic;
	private Texture2D cursorGreen;
	private Texture2D cursorRed;
    public Player2D player2D;
    public List<Vector3> playerPosList;
    public Text batteryText;
    public Text endText;
    public RaycastHit[] hits;
    public Transform CrossPrefab;
    public Transform LowBatteryPrefab;
    public int batteryCount;
    public int ammoCount;
	public GameObject SoundManager;

    private RaycastHit hit;
    private List<Vector2> mapPoints;
    private List<int> distanceTravelled;
    private Vector3 prevShadowPos;
    private Vector2 cursorGreenHotspot, cursorRedHotspot;
    private List<Object> playerShadowPrefabList;
    private List<Object> linePrefabList;
    private List<GameObject> BatteriesHitList; private List<GameObject> ammosHitList;
    private List<int> batteryUsedList;
    private List<int> batteryPickups; private List<int> ammoPickups;
    private List<int> batteryPickupsCount; private List<int> ammoPickupsCount;
    private Transform lineDynamic;
    private Transform cross;
    private Transform LowBattery;
    private int layerMask;
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
		cursorGreen = Resources.Load ("Sprites/Robot") as Texture2D;
		cursorRed = Resources.Load ("Sprites/Robot_red") as Texture2D;

        mapPoints = GameManager.Instance.mapPoints;
        mapPoints.Clear();

        distanceTravelled = GameManager.Instance.distanceTravelled;
        distanceTravelled.Clear();

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

        layerMask = 1 << 12;

        cross = Instantiate(CrossPrefab, hit.point, Quaternion.identity) as Transform;
        cross.gameObject.SetActive(false);

        LowBattery = Instantiate(LowBatteryPrefab, hit.point, Quaternion.identity) as Transform;
        LowBattery.gameObject.SetActive(false);

        cursorGreenHotspot.x = cursorGreen.width / 2;
        cursorGreenHotspot.y = cursorGreen.height / 2;

        cursorRedHotspot.x = cursorRed.width / 2;
        cursorRedHotspot.y = cursorRed.height / 2;

        GameManager.Instance.width2DPlane = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect.width;
        GameManager.Instance.height2DPlane = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect.height;

        int currentBattery = System.Int32.Parse(batteryText.text);
        thresholdDistance = (currentBattery / GameManager.Instance.batteryDepletionRate);

        GameManager.Instance.headShots = 0;
        GameManager.Instance.totalEnemiesKilled = 0;

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
    }


    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;

        //Handles.DrawWireArc(prevShadowPos, Vector3.back, mousePos, 360, thresholdDistance);

        LineRenderer LineR = lineDynamic.GetComponent<LineRenderer>();
        LineR.SetPosition(0, prevShadowPos);

        int travelDist = (int)Mathf.Ceil(Vector3.Distance(prevShadowPos, mousePos));
        int currentBattery = System.Int32.Parse(batteryText.text);

        if ((countObjects(mousePos, "wallColliders") == 0))                                        //green
        {
            Cursor.SetCursor(cursorGreen, cursorGreenHotspot, CursorMode.Auto);
            LineR.SetColors(Color.black, Color.black);
            // show red cursor if we cannot have battery to move there
            LineR.SetPosition(1, mousePos);
            LowBattery.gameObject.SetActive(false);
            cross.gameObject.SetActive(false);
            if (currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) < 0)
            {
                Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
                LineR.SetColors(Color.red, Color.red);


                LineR.SetPosition(1, (((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                LowBattery.gameObject.SetActive(true);
                LowBattery.position = ((((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
            }
            //Debug.Log(mousePos); 
        }
        else                                                                                        //red cuz of wall
        {
            Cursor.SetCursor(cursorRed, cursorRedHotspot, CursorMode.Auto);
            LineR.SetColors(Color.red, Color.red);
            LowBattery.gameObject.SetActive(false);
            cross.gameObject.SetActive(true);

            Physics.Raycast(prevShadowPos, (mousePos - prevShadowPos).normalized, out hit, (mousePos - prevShadowPos).magnitude, layerMask);
            LineR.SetPosition(1, hit.point);
            cross.position = hit.point;

            if (currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) < 0)
            {
                if ((hit.point - prevShadowPos).magnitude > thresholdDistance)
                {
                    cross.gameObject.SetActive(false);
                    LowBattery.gameObject.SetActive(true);
                    LowBattery.position = ((((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                    LineR.SetPosition(1, (((mousePos - prevShadowPos).normalized) * thresholdDistance) + prevShadowPos);
                }
            }

            //Debug.Log(hit.transform.name);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoPrevMove();
        }
    }

    private void UndoPrevMove()
    {
        endText.gameObject.SetActive(false);
        if (playerShadowPrefabList.Count > 0)
        {
			SoundManager.GetComponent<Audio> ().Undo();
            mapPoints.RemoveAt(mapPoints.Count - 1);

            Transform prevShadow = playerShadowPrefabList[playerShadowPrefabList.Count - 1] as Transform;
            Destroy(prevShadow.gameObject);
            playerShadowPrefabList.RemoveAt(playerShadowPrefabList.Count - 1);

            Transform prevLine = linePrefabList[linePrefabList.Count - 1] as Transform;
            Destroy(prevLine.gameObject);
            linePrefabList.RemoveAt(linePrefabList.Count - 1);

            //--------------------------------------------------------Undo

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

            currentBattery = System.Int32.Parse(batteryText.text);
            thresholdDistance = (currentBattery / GameManager.Instance.batteryDepletionRate);
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

            distanceTravelled.RemoveAt(distanceTravelled.Count - 1);
        }
    }

    void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0.0f;

        //Debug.Log(countObjects(worldPos, "wallColliders"));

		if (countObjects (worldPos, "wallColliders") == 0) {//if(!checkForWall(worldPos))//if (!checkForObject(worldPos)  || (hit.transform.parent.name != "wallColliders"))//if (!checkForObstruction(worldPos))
			// reduce batteryzz
			int travelDist = (int)Mathf.Ceil (Vector3.Distance (prevShadowPos, worldPos));
			distanceTravelled.Add (travelDist);
			int currentBattery = System.Int32.Parse (batteryText.text);
			if (currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate) >= 0) {
				SoundManager.GetComponent<Audio> ().MouseClicked ();
				int batteryLeft = currentBattery - (travelDist * GameManager.Instance.batteryDepletionRate);
				int batteriesPicked = 0;
				if (countObjects (worldPos, "Batteries") > 0) {                                         //battery detection
					//Debug.Log(countObjects(worldPos, "Batteries"));
					for (int i = 0; i < hits.Length; i++) {
						if (hits [i].transform.parent.name == "Batteries") {
							//Debug.Log(i);
							//Destroy(hits[i].transform.gameObject);
							BatteriesHitList.Add (hits [i].transform.gameObject);
							hits [i].transform.gameObject.SetActive (false);
							batteryPickups.Add (50);
							batteryLeft += 50;
							batteriesPicked++;
						}
					}

					/*  if (batteryLeft > 100)
                      {
                          batteryLeft = batteryLeft - (batteryLeft % 100);
                      }*/
				}
				batteryPickupsCount.Add (batteriesPicked);
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
				batteryUsedList.Add ((travelDist * GameManager.Instance.batteryDepletionRate));
				batteryText.text = batteryLeft.ToString ();

				int ammosPicked = 0;
				if (countObjects (worldPos, "Ammos") > 0) {                                         //Ammo detection
					//Debug.Log(countObjects(worldPos, "Batteries"));
					for (int i = 0; i < hits.Length; i++) {
						if (hits [i].transform.parent.name == "Ammos") {
							//Debug.Log(i);
							//Destroy(hits[i].transform.gameObject);
							ammosHitList.Add (hits [i].transform.gameObject);
							hits [i].transform.gameObject.SetActive (false);
							ammoPickups.Add (10);
							//batteryLeft += 10;
							ammosPicked++;
						}
					}

					/*  if (batteryLeft > 100)
                      {
                          batteryLeft = batteryLeft - (batteryLeft % 100);
                      }*/
				}
				ammoPickupsCount.Add (ammosPicked);
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

                if(countObjects(worldPos, "Target") > 0)
                {
                    Debug.Log("End");
                    endText.gameObject.SetActive(true);
                }


				Vector2 imagePos = convertToPixels (worldPos);
				mapPoints.Add (imagePos);

				playerPosList.Add (worldPos);
				Object playerShadowprefab = Instantiate (PlayerShadowPrefab, worldPos, Quaternion.identity);
				playerShadowPrefabList.Add (playerShadowprefab);

				Transform line = Instantiate (LinePrefab, prevShadowPos, Quaternion.identity) as Transform;
				linePrefabList.Add (line);
				LineRenderer LineR = line.GetComponent<LineRenderer> ();
				LineR.SetPosition (0, prevShadowPos);
				LineR.SetPosition (1, worldPos);

				prevShadowPos = worldPos;
				currentBattery = System.Int32.Parse (batteryText.text);
				thresholdDistance = (currentBattery / GameManager.Instance.batteryDepletionRate);



			} else {
				SoundManager.GetComponent<Audio> ().WrongClick ();
			}
		} 
		else 
		{
			SoundManager.GetComponent<Audio> ().WrongClick ();
		}
    }
}
