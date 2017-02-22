using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDetection : MonoBehaviour
{

    // Use this for initialization
    private Transform player;
    private Transform payload;
    private DroneMovement droneMovementScript;
    Vector3 enemyCenter;
    void Start()
    {
        player = GameObject.Find("FPSPlayer").GetComponent<Transform>();
        payload = GameObject.Find("PayLoad").GetComponent<Transform>();
        droneMovementScript = gameObject.GetComponentInParent<DroneMovement>();
        enemyCenter = Vector3.zero;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NewPayload"))
        {
            if (droneMovementScript != null)
            {
                enemyCenter = transform.parent.position;//aiMovementScript.transform.position + (5 * Vector3.up);
                RaycastHit hit;
                if (Physics.Raycast(enemyCenter, (other.transform.position - enemyCenter).normalized, out hit, (other.transform.position - enemyCenter).magnitude))
                {
                    //Debug.DrawRay(enemyCenter, (other.transform.position - enemyCenter), Color.white);
                    //Debug.Log(hit.transform.name);
                    if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("NewPayload"))
                    {
                        droneMovementScript.Detection(other.transform);
                    }
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NewPayload"))
        {
            droneMovementScript.OutOfRange();
        }
    }

}
