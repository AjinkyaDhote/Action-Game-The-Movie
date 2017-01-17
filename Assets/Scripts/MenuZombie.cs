using UnityEngine;
using System.Collections;

public class MenuZombie : MonoBehaviour
{
    public Transform[] patrolWayPoints;

    int target;
    Animator anim;

    UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        target = 0;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.autoBraking = false;
        Patrol();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (anim.GetBool("isEnemyDead"))
        {
            agent.speed = 0;
        }
        if (target == patrolWayPoints.Length)
        {
            target = 0;
        }

        agent.destination = patrolWayPoints[target].transform.position;
        target++;
    }
}
