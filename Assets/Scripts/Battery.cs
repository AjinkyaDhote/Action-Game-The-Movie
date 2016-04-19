using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battery : MonoBehaviour {

    Text BatteryText;
    private int batteryCount = 100;
    PlayerShooting playerShootingScript;
    string batteryString;
    void Start () {
        playerShootingScript = transform.GetChild(0).GetChild(0).GetComponent<PlayerShooting>();
		BatteryText = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("BatteryText").GetComponent<Text>();
        batteryString = " " + batteryCount;
        BatteryText.text = batteryString;
        BatteryText.color = Color.blue;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery")
        {
            batteryCount += 10;
            batteryString = "" + batteryCount;
            BatteryText.text = batteryString;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Ammo")
        {
            playerShootingScript.PickupAmmo();
            Destroy(other.gameObject);
        }
    }
}
