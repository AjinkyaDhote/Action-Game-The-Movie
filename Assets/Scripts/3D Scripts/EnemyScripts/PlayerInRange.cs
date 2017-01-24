using UnityEngine;
using System.Collections;

public class PlayerInRange : MonoBehaviour
{
    AI_movement aiMovementScript;
    void Start()
    {
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "NewPayload")
        {
            if (aiMovementScript != null)
            {
                RaycastHit hit;
                Physics.Raycast(aiMovementScript.transform.position, (other.transform.position - aiMovementScript.transform.position).normalized, out hit, Mathf.Infinity);
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