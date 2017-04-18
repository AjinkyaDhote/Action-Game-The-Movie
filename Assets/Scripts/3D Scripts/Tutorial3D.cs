using UnityEngine;
using System.Collections;

public class Tutorial3D : MonoBehaviour
{
    public GameObject infoWindow;
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
                infoDialogue.playInfo("You are now in the 3D level. The path you planned is now shown on the floor. Battery robot will follow the path and collect the battery canisters.");
            }
            else if (!batteryBotTrigger && gameObject.transform.name == "batteryBotHealthGreen")
            {
                batteryBotTrigger = true;
                infoDialogue.playInfo("Health bar on top of the battery robot is it's health. The battery robot's GREEN circle is the zone you need to be in to get charged from battery robot.");
            }
            else if (!batteryBotTrigger && gameObject.transform.name == "batteryBotHealthRed")
            {
                batteryBotTrigger = true;
                infoDialogue.playInfo("When you are in low charge your visuals get corrupted. And the Battery robot cicle changes to YELLOW and then RED");
            }
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
