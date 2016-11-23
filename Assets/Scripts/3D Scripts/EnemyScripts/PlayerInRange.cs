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
        if (other.tag == "Player" || other.tag == "NewPayload")
        {
            if (aiMovementScript != null)
            {
                aiMovementScript.InRange(other.transform);
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
