using UnityEngine;
using System.Collections;

public class DroneBulletDamage : MonoBehaviour
{
    PlayerHealthScript playerHealthScript;
    PayLoadHealthScript payloadHealthScript;
    private float playerDamage;
    private Rigidbody rb;
    private void Start()
    {
        playerDamage = 2.0f;
        rb = GetComponent<Rigidbody>();
        playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        payloadHealthScript = GameObject.FindGameObjectWithTag("NewPayload").transform.FindChild("PayLoadHealthBar").GetComponent<PayLoadHealthScript>();
    }

    void OnCollisionEnter(Collision other)
    {        
        if (other.collider.CompareTag("Player"))
        {
        
            if ((playerHealthScript != null))
            {
                playerHealthScript.PlayerDamage(playerDamage, 0.3f);
            }

             rb.isKinematic = true;
             Destroy(transform.gameObject);
        }

        else if (other.collider.CompareTag("NewPayload"))
        {
            if ((payloadHealthScript != null))
            {
                payloadHealthScript.PayLoadDamage(other.collider.tag);
            }
            rb.isKinematic = true;
            Destroy(transform.gameObject);
        }

        else if (other.collider.CompareTag("Wall"))
        {
            Destroy(transform.gameObject);
        }       
    } 
}
