using UnityEngine;
using System.Collections;

public class LaserDamage : MonoBehaviour
{
    
    PayLoadHealthScript payloadHealth;

	void Start ()
    {
        payloadHealth = GameObject.FindGameObjectWithTag("NewPayload").transform.GetChild(5).gameObject.GetComponent<PayLoadHealthScript>();
        
	}
	
	
	void Update ()
    {
	    

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NewPayload")
        {
            payloadHealth.PayLoadDamage(gameObject.tag);
        }        
    }
}
