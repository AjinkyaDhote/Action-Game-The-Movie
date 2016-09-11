using UnityEngine;
using System.Collections;

public class PlayerInRange : MonoBehaviour
{
    AI_movement aiMovementScript;
    //EnemyThrow enemyThrowScript;
    void Start()
    {
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //int a = 0;
            if (aiMovementScript != null)
            {
                aiMovementScript.InRange();

            }
            //if (enemyThrowScript != null)
            //{
            //    enemyThrowScript.InRange();
            //}
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
            //if (enemyThrowScript != null)
            //{
            //    enemyThrowScript.OutOfRange();
            //}
        }
    }
}
