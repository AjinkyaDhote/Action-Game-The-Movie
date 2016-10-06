using UnityEngine;
using System.Collections;

public class PayloadGun : MonoBehaviour {

    Transform payLoadtransform;
    //Canvas canvas;

	void Start ()
    {
        payLoadtransform = GameObject.FindGameObjectWithTag("PayLoad").GetComponent<Transform>(); 	

	}
	
	void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PayLoad")
        {
            Debug.Log("Collided with Payload");
        }
        
    }
}
