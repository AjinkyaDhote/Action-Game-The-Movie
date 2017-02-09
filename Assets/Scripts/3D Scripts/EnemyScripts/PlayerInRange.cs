using UnityEngine;
using System.Collections;

public class PlayerInRange : MonoBehaviour
{
    AI_movement aiMovementScript;
    Vector3 enemyCenter;
    GameObject sight;
    void Start()
    {
        sight = transform.parent.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "NewPayload")
        {
            if (aiMovementScript != null)
            {
                enemyCenter = sight.transform.position; //aiMovementScript.transform.position + (5 * Vector3.up);
                RaycastHit hit;
                Physics.Raycast(enemyCenter, (other.transform.position - enemyCenter).normalized, out hit, (other.transform.position - enemyCenter).magnitude);
                //Debug.DrawRay(enemyCenter, (other.transform.position - enemyCenter), Color.green);
                //Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("NewPayload"))
                {
                    aiMovementScript.InRange(other.transform);
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.tag == "NewPayload")
        {
            if (aiMovementScript != null)
            {
                aiMovementScript.OutOfRange();
            }
        }
    }
}