using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour
{
	GameObject player;
	Animator anim;

	NavMeshAgent agent;
	[HideInInspector]
	public bool isPlayerSeen;
	bool isPlayerInRange;
	public AudioClip zombieWalk;

	public float radius;
	private float minRadius;
	private Vector3 initialPos;

	Collider enemyBodyCollider, playerCollider, enemyHeadCollider;

	void Start()
	{
		Random.seed = 42;
		initialPos = gameObject.transform.position;
		radius = 10;
		minRadius = 2;

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

	Vector3 getRandomVector()
	{
		bool hit = true;
		Vector3 randomPoint;
		Vector3 direction;
		do
		{
			AudioSource.PlayClipAtPoint(zombieWalk,new Vector3(transform.position.x,transform.position.y,transform.position.z));
			randomPoint.x = (Random.value * (radius - minRadius)) + minRadius;
			randomPoint.y = 1.0f;
			randomPoint.z = (Random.value * (radius - minRadius)) + minRadius;

			randomPoint += initialPos;

			direction = randomPoint - gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
			hit = Physics.Raycast(gameObject.transform.position + new Vector3( 0.0f, 1.0f, 0.0f ), direction.normalized, direction.magnitude);
			//Debug.DrawRay(gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), direction.normalized, Color.red, 1);

		} while (hit);

		return randomPoint;
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
                    agent.speed = 0;
					transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);

				}
				else
				{
                    //transform.position += transform.forward * 9 * Time.deltaTime;
                    agent.speed = 7;
                    agent.destination = player.transform.position;

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
		//agent.speed = 0;
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

		agent.destination = getRandomVector();
	}
}