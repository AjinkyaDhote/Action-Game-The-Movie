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
            if (aiMovementScript != null)
            {
                RaycastHit hit;
                Physics.Raycast(aiMovementScript.transform.position, (other.transform.position - aiMovementScript.transform.position).normalized, out hit, Mathf.Infinity);
                //Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("NewPayload"))
                {
                    aiMovementScript.Detection(other.transform);
                }
            }
        }
    }
}
