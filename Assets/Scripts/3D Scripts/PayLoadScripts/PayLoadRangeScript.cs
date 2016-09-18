using UnityEngine;
using System.Collections;

public class PayLoadRangeScript : MonoBehaviour {

    public GameObject player;


    private PlayerHealthScript playerHealth;
    private bool outOfRange;
    private  BoxCollider boxCollider;
    private CapsuleCollider playerCollider;
	
	void Start ()
    {
        playerHealth = player.GetComponent<PlayerHealthScript>();
        outOfRange = false;
        boxCollider = GetComponent<BoxCollider>();
        //playerCollider = player.GetComponent<CapsuleCollider>();
	}
	
	
	void Update ()
    {
        //Physics.IgnoreCollision(boxCollider, playerCollider);
        if(outOfRange)
        {
            playerHealth.PlayerDamage(0.01f);
        }	

	}
 
    //void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            outOfRange = false;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            outOfRange = true;
            //playerHealth.PlayerDamage();
        }
    }
}
