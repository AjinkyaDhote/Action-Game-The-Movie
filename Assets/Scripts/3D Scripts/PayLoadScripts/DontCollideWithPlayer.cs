using UnityEngine;
using System.Collections;

public class DontCollideWithPlayer : MonoBehaviour
{
  		  
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().position = transform.position;
        }
    }
}
