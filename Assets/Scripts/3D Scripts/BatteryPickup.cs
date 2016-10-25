using UnityEngine;
using System.Collections;

public class BatteryPickup : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NewPayload"))
        {
            gameObject.SetActive(false);
        }
    }


}
