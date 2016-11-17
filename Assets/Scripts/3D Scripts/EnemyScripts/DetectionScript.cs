using UnityEngine;
using System.Collections;

public class DetectionScript : MonoBehaviour
{
    AI_movement aiMovementScript;
    //EnemyThrow enemyThrowScript;

    void Start()
    {
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NewPayload"))
        {
            if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
            {
                aiMovementScript.Detection(other.transform);
            }          
        }
    }
}
