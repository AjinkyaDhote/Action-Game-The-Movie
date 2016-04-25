using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
	PlayerShooting playerShootingScript;
	public int batteryPickedUp;

	void Start()
	{
		batteryPickedUp = 0;
		playerShootingScript = transform.GetChild(0).GetChild(0).GetComponent<PlayerShooting>();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Battery")
		{
			batteryPickedUp += 50;
			Destroy(other.gameObject);
		}
		else if (other.tag == "Ammo")
		{
			playerShootingScript.PickupAmmo();
			Destroy(other.gameObject);
		}
	}
}
