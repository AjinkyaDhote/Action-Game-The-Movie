using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCardPickupScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewPayload"))
        {
            LevelManager3D.accessCardCount++;
            gameObject.SetActive(false);
        }
    }
}
