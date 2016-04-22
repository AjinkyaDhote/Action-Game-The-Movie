using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour
{
    public Transform[] patrolWayPoints;

    GameObject player;
    int target;
    Animator anim;

    NavMeshAgent agent;
    [HideInInspector]
    public bool isPlayerSeen;
    bool isPlayerInRange;

    Collider enemyBodyCollider, playerCollider, enemyHeadCollider;

    void Start()
    {
        target = 0;
        isPlayerSeen = false;
        isPlayerInRange = false;
        
        agent = GetComponent<NavMeshAgent>();        
        anim = GetComponent<Animator>();          
        enemyBodyCollider = transform.GetChild(0).GetChild(2).GetComponent<Collider>();
        enemyHeadCollider = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();

        agent.autoBraking = false;
        Patrol();
    }

    void Update()
    {
		if (anim.GetBool ("isPlayerDead")) {
		
			agent.speed = 0;
		} 
		else {
			Physics.IgnoreCollision(enemyBodyCollider, playerCollider);
            Physics.IgnoreCollision(enemyHeadCollider, playerCollider);

            if (isPlayerSeen)
			{
				transform.LookAt(player.transform);
				if (isPlayerInRange)
				{
					transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);

				}
				else
				{
					transform.position += transform.forward * 9 * Time.deltaTime;

				}       
			}
			else
			{
				if (agent.remainingDistance < 0.5f)
				{
					Patrol();
				}     
			}
       }
    }
    void DamagePlayer()
    {
        player.GetComponent<PlayerHealthScript>().PlayerDamage();
        player.GetComponent<HitRadial>().createNewRadial(this.gameObject);
    }
    public void Detection()
    {
        transform.LookAt(player.transform);
        isPlayerSeen = true;
        anim.SetBool("isPlayerSeen", true);
        agent.speed = 0;
    }
    public void InRange()
    {
        anim.SetBool("isPlayerInRange", true);
        isPlayerInRange = true;
    }
    public void OutOfRange()
    {
        anim.SetBool("isPlayerInRange", false);
        isPlayerInRange = false;
    }
    void Patrol()
    {
        if (anim.GetBool("isPlayerDead"))
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
