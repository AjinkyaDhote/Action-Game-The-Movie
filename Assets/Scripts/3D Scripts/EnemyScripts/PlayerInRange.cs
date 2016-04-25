using UnityEngine;
using System.Collections;

public class PlayerInRange : MonoBehaviour{
    AI_movement aiMovementScript;
    EnemyThrow enemyThrowScript;
    void Start()
    {
        if (transform.parent.CompareTag("SmallEnemy"))
        {
            aiMovementScript = transform.parent.GetComponent<AI_movement>();
        }
        else
        {
            enemyThrowScript = transform.parent.GetComponent<EnemyThrow>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (aiMovementScript != null)
            {
                aiMovementScript.InRange();
            }
            if (enemyThrowScript != null)
            {
                enemyThrowScript.InRange();
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
            if (enemyThrowScript != null)
            {
                enemyThrowScript.OutOfRange();
            }
        }
    }
}
