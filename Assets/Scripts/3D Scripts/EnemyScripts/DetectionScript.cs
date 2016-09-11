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
        if (other.CompareTag("Player"))
        {
            if (aiMovementScript != null)
            {
                aiMovementScript.Detection();
            }
            //if (enemyThrowScript != null)
            //{
            //    enemyThrowScript.Detection();
            //}
        }
    }
}
