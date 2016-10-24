using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager2D : MonoBehaviour
{
    public SpriteRenderer backgroudSprite;

    public GameObject batteryAmmoLayer;
    public GameObject map;

    public Sprite bg2, bg3, bg4, bg5, bg6;

    public GameObject secondBattery;
    public GameObject undoBattery;
    public GameObject ammoInRange;
    public GameObject batteryIsImp;
    public GameObject highLowDensity;
    public GameObject target;
    public GameObject StoryColliders;
    public GameObject StopPoints;
    public GameObject dialogueBox;

    public GameObject player;

    public GameObject dynamicBattery;

    private enum stage { FIRST_STAGE, SECOND_STAGE, THIRD_STAGE, FOURTH_STAGE, FIFTH_STAGE, NO_OP };

    private stage currentStage = stage.FIRST_STAGE;

    void Awake()
    {
        MapScript mapScript = map.GetComponent<MapScript>();

        GameManager.Instance.ammoPosList.Clear();
        GameManager.Instance.batteryPosList.Clear();

        for (int i = 0; i < batteryAmmoLayer.transform.childCount; i++)
        {
            if (batteryAmmoLayer.transform.GetChild(i).tag == "Battery")
            {
                Vector3 batteryPos = batteryAmmoLayer.transform.GetChild(i).position;
                Vector2 imagePos = mapScript.convertToPixels(batteryPos);
                GameManager.Instance.batteryPosList.Add(imagePos);
            }
            else if (batteryAmmoLayer.transform.GetChild(i).tag == "Ammo")
            {
                Vector3 ammoPos = batteryAmmoLayer.transform.GetChild(i).position;
                Vector2 imagePos = mapScript.convertToPixels(ammoPos);
                GameManager.Instance.ammoPosList.Add(imagePos);
            }
            else
            {
                for (int j = 0; j < batteryAmmoLayer.transform.GetChild(i).childCount; j++)
                {
                    if (batteryAmmoLayer.transform.GetChild(i).GetChild(j).tag == "Battery")
                    {
                        Vector3 batteryPos = batteryAmmoLayer.transform.GetChild(i).GetChild(j).position;
                        Vector2 imagePos = mapScript.convertToPixels(batteryPos);
                        GameManager.Instance.batteryPosList.Add(imagePos);
                    }
                    else if (batteryAmmoLayer.transform.GetChild(i).GetChild(j).tag == "Ammo")
                    {
                        Vector3 ammoPos = batteryAmmoLayer.transform.GetChild(i).GetChild(j).position;
                        Vector2 imagePos = mapScript.convertToPixels(ammoPos);
                        GameManager.Instance.ammoPosList.Add(imagePos);
                    }
                }
            }
        }
    }

    void Start()
    {
        secondBattery.active = false;
        undoBattery.active = false;
        ammoInRange.active = false;
        batteryIsImp.active = false;
        highLowDensity.active = false;
        target.active = false;
        dialogueBox.active = false;
    }

    public void Resume()
    {
        map.GetComponent<BoxCollider2D>().enabled = true;
        map.GetComponent<MapScript>().enabled = true;
    }

    void Update()
    {
        switch (currentStage)
        {
            case stage.FIRST_STAGE:
                {
                    if (Vector3.Distance(player.transform.position, StopPoints.transform.GetChild(0).position) < 0.75f)
                    {
                        //print("First Step completed.");
                        currentStage = stage.SECOND_STAGE;
                        backgroudSprite.sprite = bg2;
                        StoryColliders.gameObject.transform.GetChild(0).gameObject.SetActiveRecursively(false);
                        secondBattery.active = true;
                        dialogueBox.active = true;

                        dialogueBox.GetComponent<DialogManager2DLevel1>().playCutScene1();
                        map.GetComponent<MapScript>().enabled = false;
                        map.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                break;
            case stage.SECOND_STAGE:
                {
                    if (dynamicBattery.GetComponent<TextMesh>().text.Equals("0"))
                    {
                        //print("Entered Second Stage.");
                        currentStage = stage.THIRD_STAGE;
                        backgroudSprite.sprite = bg3;
                        StoryColliders.gameObject.transform.GetChild(1).gameObject.SetActiveRecursively(false);
                        undoBattery.active = true;
                    }
                }
                break;

            case stage.THIRD_STAGE:
                {
                    if (Vector3.Distance(player.transform.position, StopPoints.transform.GetChild(1).position) < 0.75f)
                    {
                        currentStage = stage.FOURTH_STAGE;
                        backgroudSprite.sprite = bg4;
                        StoryColliders.gameObject.transform.GetChild(2).gameObject.SetActiveRecursively(false);
                        ammoInRange.active = true;
                    }
                }
                break;

            case stage.FOURTH_STAGE:
                {
                    if (player.gameObject.transform.position.x > 10.0f || player.gameObject.transform.position.y > -7.7f)
                    {
                        currentStage = stage.FIFTH_STAGE;
                        backgroudSprite.sprite = bg5;
                        StoryColliders.gameObject.transform.GetChild(3).gameObject.SetActiveRecursively(false);
                        batteryIsImp.active = true;
                    }
                }
                break;

            case stage.FIFTH_STAGE:
                {
                    if (player.gameObject.transform.position.y > 5.0f)
                    {
                        currentStage = stage.NO_OP;
                        backgroudSprite.sprite = bg6;
                        StoryColliders.gameObject.transform.GetChild(4).gameObject.SetActiveRecursively(false);
                        highLowDensity.active = true;
                        target.active = true;
                    }
                }
                break;

            case stage.NO_OP:
                {

                }
                break;
        }
    }
}
