using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
	PlayerShooting playerShootingScript;
    public int batteryPickedUp;
    //Text ammoCollected;
	void Start()
	{
		batteryPickedUp = 0;
		playerShootingScript = transform.GetChild(0).GetChild(0).GetComponent<PlayerShooting>();
        //ammoCollected = transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas").FindChild("AmmoPickupedText").GetComponent<Text>();
        //ammoCollected.gameObject.SetActive(false);
    }
	void OnTriggerEnter(Collider other)
	{
		/*if (other.tag == "Battery")
		{
			batteryPickedUp += 50;
			Destroy(other.gameObject);
		}*/
		if (other.tag == "Ammo")
		{          
            //ammoCollected.gameObject.SetActive(true);
           // ammoCollected.text = "Plus 10 Ammo Collected";
            playerShootingScript.PickupAmmo();
            //StartCoroutine(DisableAmmoCollectedText());
			Destroy(other.gameObject);
		}
	}
    //IEnumerator DisableAmmoCollectedText()
    //{
    //    yield return new WaitForSeconds(2.0f); 
    //   ammoCollected.gameObject.SetActive(false);
    //}
}
