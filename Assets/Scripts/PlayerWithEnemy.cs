using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWithEnemy : MonoBehaviour {

    Text HealthText;
    Text AmmoText;
    Text BatteryText;
    public int bulletCount  = 0;
    private int healthCount  = 100;
    private int batteryCount = 100;
    //private BoxCollider col;
    string bulletsString;
    string batteryString;
    string healthString;

    // Use this for initialization
    void Start () {
        /*HealthBar = transform.FindChild("Main Camera").transform.FindChild("FPS UI Canvas").FindChild("healthBar").GetComponent<Image>();
		EnergyBar = transform.FindChild("Main Camera").transform.FindChild("FPS UI Canvas").FindChild("energyBar").GetComponent<Image>();*/
		AmmoText    = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("AmmoText").GetComponent<Text>();
		HealthText  = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("HealthText").GetComponent<Text>();
		BatteryText = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("BatteryText").GetComponent<Text>();
        bulletsString = " " + bulletCount;
        healthString  = " " + healthCount;
        batteryString = " " + batteryCount;
        AmmoText.text = bulletsString;
        AmmoText.color = Color.red;
        HealthText.text = healthString;
        BatteryText.text = batteryString;
    }
    int counterForBatteryDrain = 0;

    void Awake()
    {

        //col = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update () {

        counterForBatteryDrain++;

        if (Input.GetMouseButtonDown(0))
        {
            if (bulletCount > 0)
            {
                //Debug.Log("Pressed left click.");
                bulletCount -= 1;
                bulletsString = " " + bulletCount;
                AmmoText.text = bulletsString;
                if (bulletCount == 0)
                {
                    AmmoText.color = Color.red;
                }
            }
            else
            {
                AmmoText.color = Color.red;
            }
        }

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
            bulletCount += 10;
            bulletsString = " " + bulletCount;
            AmmoText.text = bulletsString;
            AmmoText.color = Color.black;
            Destroy(other.gameObject);
        }
    }
}
