using UnityEngine;
using System.Collections;


class PlayerInRangeOfPayload : MonoBehaviour
{
    PayLoadRangeScript payLoadRange;

    void Start()
    {
        payLoadRange = GetComponentInParent<PayLoadRangeScript>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            payLoadRange.outOfRange = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            payLoadRange.outOfRange = true;
            //playerHealth.PlayerDamage();
        }
    }
}

