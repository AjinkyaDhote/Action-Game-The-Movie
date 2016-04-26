using UnityEngine;
using System.Collections;

public class DetectionScript : MonoBehaviour {
    AI_movement aiMovementScript;
    EnemyThrow enemyThrowScript;

    void Start()
    {
        if(transform.parent.CompareTag("SmallEnemy"))
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
        if(other.CompareTag("Player"))
        {
            if (aiMovementScript != null)
            {
                aiMovementScript.Detection();
            }
            if (enemyThrowScript != null)
            {
                enemyThrowScript.Detection();
            }
        }
	}
		
}
