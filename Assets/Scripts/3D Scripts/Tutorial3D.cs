using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial3D : MonoBehaviour
{
    public GameObject infoWindow;

    public Sprite protectPayload;
    public Sprite stayInRange;
    public Sprite followPath;

    private InfoDialogue infoDialogue;

    bool startTriggerDone = false;
    bool batteryBotTrigger = false;
    bool enemyIntroTrigger = false;

    void Awake()
    {
        infoDialogue = infoWindow.GetComponent<InfoDialogue>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.countDownDone && other.transform.name == "PayloadWithAnimation")
        {
            if (!startTriggerDone && gameObject.transform.name == "StartTrigger")
            {
                startTriggerDone = true;
                infoDialogue.playInfo("Payload will follow the path and collect the battery canisters.");
            }
            else if (!batteryBotTrigger && gameObject.transform.name == "batteryBotHealthGreen")
            {
                batteryBotTrigger = true;
                //Health bar on top of the battery robot is it's health.
                infoDialogue.playInfoTwoImage("Stay in range to get charged by the Payload.", stayInRange, protectPayload);
            }
            //else if (!batteryBotTrigger && gameObject.transform.name == "batteryBotHealthRed")
            //{
            //    batteryBotTrigger = true;
            //    infoDialogue.playInfo("When you are in low charge your visuals get corrupted.");
            //}
        }
        else if (GameManager.Instance.countDownDone && other.transform.name == "FPSPlayer")
        {
            if (!enemyIntroTrigger && gameObject.transform.name == "enemyIntro")
            {
                enemyIntroTrigger = true;
                infoDialogue.playInfo("Be cautious enemy sentinels may be around. Try to aim at their heads.");
            }
        }
    }
}
