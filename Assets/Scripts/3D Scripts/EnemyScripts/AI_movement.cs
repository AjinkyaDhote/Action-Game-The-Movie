﻿using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour
{
	public float enemyWalkSpeed;
	public float enemyRunSpeed;

	GameObject player;
	//Animator anim;
	PlayerHealthScript playerHealth;

	GameObject hitRadialPrefab;
	GameObject hitRadial;

	NavMeshAgent agent;
	[HideInInspector]
	
	bool isPlayerInRange;
	Vector3 resetPositionForInRange;

	public float radius;
	private float minRadius;
	private Vector3 initialPos;
	private AudioSource zombieWalk;

	private Vector3[] randomVectors;
	private Random r;
	Collider enemyBodyCollider, playerCollider, enemyHeadCollider;

    private bool isPlayerDead;
    public bool isPlayerSeenA;
    private bool isPlayerInRangeA;



    void Start()
	{
		randomVectors = new Vector3[8];

		randomVectors[0] = new Vector3(1.0f, 1.0f, 0.0f);
		randomVectors[1] = new Vector3(1.0f, 1.0f, 1.0f);
		randomVectors[2] = new Vector3(0.0f, 1.0f, 1.0f);
		randomVectors[3] = new Vector3(-1.0f, 1.0f, 1.0f);
		randomVectors[4] = new Vector3(-1.0f, 1.0f, 0.0f);
		randomVectors[5] = new Vector3(-1.0f, 1.0f, -1.0f);
		randomVectors[6] = new Vector3(0.0f, 1.0f, -1.0f);
		randomVectors[7] = new Vector3(1.0f, 1.0f, -1.0f);

		hitRadialPrefab = Resources.Load("HitRadialPrefab/HitRadial") as GameObject;
		Random.seed = 42;
		initialPos = gameObject.transform.position;
		radius = 10;
		minRadius = 2;

		
		isPlayerInRange = false;

		zombieWalk = GetComponent<AudioSource>();
		agent = GetComponent<NavMeshAgent>();
		//anim = GetComponent<Animator>();
		enemyBodyCollider = transform.GetChild(2).GetComponent<Collider>();
		enemyHeadCollider = transform.GetChild(3).GetComponent<Collider>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealthScript>();
		playerCollider = player.GetComponent<Collider>();
		agent.speed = enemyWalkSpeed;
		Patrol();
	}

	Vector3 GetRandomVector()
	{
		bool hit = true;
		Vector3 randomPoint;
		Vector3 direction;
		int randomIndex = (int)Random.Range(0, 8);

		do
		{
			//randomPoint.x = (Random.value * (radius - minRadius)) + minRadius;
			//randomPoint.y = 1.0f;
			//randomPoint.z = (Random.value * (radius - minRadius)) + minRadius;

			randomIndex = (randomIndex + 1) % 8;
			randomPoint = randomVectors[randomIndex];
			randomPoint *= 4;
			randomPoint += initialPos;

			direction = randomPoint - gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
			hit = Physics.Raycast(gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), direction.normalized, direction.magnitude);
			//Debug.DrawRay(gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), direction.normalized, Color.red, 1);

		} while (hit);

		return randomPoint;
	}
	void Update()
	{	
		if (isPlayerDead)
		{
			agent.speed = 0;
			zombieWalk.Stop();
		}
		else
		{
			Physics.IgnoreCollision(enemyBodyCollider, playerCollider);
			Physics.IgnoreCollision(enemyHeadCollider, playerCollider);

			if (isPlayerSeenA)
			{
				transform.LookAt(player.transform);
				if (isPlayerInRange)
				{
					agent.speed = 0;
					transform.position = resetPositionForInRange;
					transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
				}
				else
				{
					agent.speed = enemyRunSpeed;
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
		playerHealth.PlayerDamage();
		hitRadial = Instantiate(hitRadialPrefab);
		hitRadial.transform.SetParent(player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas"));
		hitRadial.GetComponent<HitRadial>().StartRotation(transform);
		Destroy(hitRadial, 2.0f);
	}
	public void Detection()
	{
		transform.LookAt(player.transform);
		
        isPlayerSeenA = true;
	}
	public void InRange()
	{
        isPlayerSeenA = true;
		resetPositionForInRange = transform.position;
		isPlayerInRange = true;
	}
	public void OutOfRange()
	{
        isPlayerSeenA = false; 
		isPlayerInRange = false;
	}
	void Patrol()
	{
		if (isPlayerDead)
		{
			agent.speed = 0;
			zombieWalk.Stop();
		}
		agent.destination = GetRandomVector();
	}
}  