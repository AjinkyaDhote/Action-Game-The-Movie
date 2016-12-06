using UnityEngine;
using System.Collections;

public class LaserDamage : MonoBehaviour
{    
    PayLoadHealthScript payloadHealth;
    PlayerHealthScript playerHealth;

	void Start ()
    {
        payloadHealth = GameObject.FindGameObjectWithTag("NewPayload").transform.GetChild(5).gameObject.GetComponent<PayLoadHealthScript>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();        
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

    void OnTriggerStay(Collider other)
    {
        if( other.gameObject.tag == "Player")
        {
            playerHealth.PlayerDamage(5f, 0.04f, gameObject.name);
        }
    }

}
