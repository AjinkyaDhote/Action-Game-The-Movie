using UnityEngine;
using System.Collections;

public class DroneBulletDamage : MonoBehaviour
{
    PlayerHealthScript playerHealthScript;
    PayLoadHealthScript payloadHealthScript;
    public float playerDamage;
    public float payLoadDamage;
    private Rigidbody rb;
    GameObject hitRadialPrefab;
    GameObject hitRadial;
    GameObject player;

    private void Start()
    {
        playerDamage = 2.0f;
        rb = GetComponent<Rigidbody>();
        playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        payloadHealthScript = GameObject.FindGameObjectWithTag("NewPayload").transform.FindChild("PayLoadHealthBar").GetComponent<PayLoadHealthScript>();
        hitRadialPrefab = Resources.Load("HitRadialPrefab/HitRadial") as GameObject;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void OnCollisionEnter(Collision other)
    {        
        if (other.collider.CompareTag("Player"))
        {        
            if ((playerHealthScript != null))
            {
                playerHealthScript.PlayerDamage(playerDamage, 0.3f);
                hitRadial = Instantiate(hitRadialPrefab);
                hitRadial.transform.SetParent(player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas"));
                hitRadial.GetComponent<HitRadial>().StartRotation(transform);
                Destroy(hitRadial, 2.0f);
            }
             //rb.isKinematic = true;
             Destroy(transform.gameObject);
        }

        else if (other.collider.CompareTag("NewPayload"))
        {
            if ((payloadHealthScript != null))
            {
                payloadHealthScript.PayLoadDamage(gameObject.tag);
            }
            //rb.isKinematic = true;
            Destroy(transform.gameObject);
        }

        else if (other.collider.CompareTag("Wall"))
        {
            Destroy(transform.gameObject);
        }

        Destroy(transform.gameObject, 3f);
    } 

    void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }

}
