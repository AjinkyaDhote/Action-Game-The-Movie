using UnityEngine;
using System.Collections;

public class PlayerInRange : MonoBehaviour
{
    AI_movement aiMovementScript;
    void Start()
    {
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (aiMovementScript != null)
            {
                Physics.Raycast(transform.position, (other.gameObject.transform.position - transform.position).normalized, out aiMovementScript.raycastHit, Mathf.Infinity);
                if (aiMovementScript.raycastHit.collider.gameObject.tag == "Player")
                {
                    aiMovementScript.InRange(other.transform);
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (aiMovementScript != null)
            {
                aiMovementScript.OutOfRange();
            }
        }
    }
}
