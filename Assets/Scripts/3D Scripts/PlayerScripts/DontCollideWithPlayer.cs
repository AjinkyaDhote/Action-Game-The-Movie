using UnityEngine;
using System.Collections;

public class DontCollideWithPlayer : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //playerRigidbody.velocity = playerRigidbody.angularVelocity = Vector3.zero;
        //playerRigidbody.position = transform.position;
    }

    void OnCollsionEnter(Collision collision)
    {
       if(collision.collider.CompareTag("PayLoad"))
        {
            //playerRigidbody.velocity = playerRigidbody.angularVelocity = Vector3.zero;
            //playerRigidbody.position = transform.position;
        }
    }

}
