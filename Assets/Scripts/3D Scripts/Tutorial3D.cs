using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial3D : MonoBehaviour
{
    public GameObject infoWindow;

    public Sprite protectPayload;
    public Sprite stayInRange;
    public Sprite accessCard;
    public Sprite collectAmmo;
    public Sprite enemyWeakSpot;
    public Sprite shotPanel;

    private InfoDialogue infoDialogue;

    //bool startTriggerDone = false;
    bool batteryBotTrigger = false;
    bool enemyIntroTrigger = false;
    bool ammoIntroTrigger = false;
    bool accessCardTrigger = false;
    bool shotPanelTrigger = false;

    void Awake()
    {
        infoDialogue = infoWindow.GetComponent<InfoDialogue>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.countDownDone && other.transform.name == "PayloadWithAnimation")
        {
            //if (!startTriggerDone && gameObject.transform.name == "StartTrigger")
            //{
            //    startTriggerDone = true;
            //    infoDialogue.playInfo("Payload will follow the path and collect the batteries.");
            //}
            if (!batteryBotTrigger && gameObject.transform.name == "batteryBotHealthGreen")
            {
                batteryBotTrigger = true;
                //Health bar on top of the battery robot is it's health.
                infoDialogue.playInfoOneImage("Stay in range to get charged by the Payload.", stayInRange, "Stay In Range");
            }
            else if (!shotPanelTrigger && gameObject.transform.name == "ShootPanel")
            {
                shotPanelTrigger = true;
                infoDialogue.playInfoOneImage("You can shoot the Laser Console to disable traps.", shotPanel, "Shoot Panel");
            }
        }
        else if (GameManager.Instance.countDownDone && other.transform.name == "FPSPlayer")
        {
            if (!enemyIntroTrigger && gameObject.transform.name == "enemyIntro")
            {
                enemyIntroTrigger = true;
                infoDialogue.playInfoTwoImage("Be cautious enemy robots may be around. Try to aim at their heads.", protectPayload, "Protect Payload", enemyWeakSpot, "Enemy Weak Spot");
            }
            else if (!ammoIntroTrigger && gameObject.transform.name == "AmmoPickup")
            {
                ammoIntroTrigger = true;
                infoDialogue.playInfoOneImage("Dont forget to pick up the ammo.", collectAmmo, "Collect Ammo");
            }
            else if (!accessCardTrigger && gameObject.transform.name == "AccessCard")
            {
                accessCardTrigger = true;
                infoDialogue.playInfoOneImage("Use the access card to unlock traps.", accessCard, "Use Access Card");
            }
            else if (!shotPanelTrigger && gameObject.transform.name == "ShootPanel")
            {
                shotPanelTrigger = true;
                infoDialogue.playInfoOneImage("You can shoot Laser Console to disable traps.", shotPanel, "Shoot Panel");
            }
        }
    }
}
