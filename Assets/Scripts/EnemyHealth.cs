using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth;
	private Animator anim;
	private NavMeshAgent agent;

	int delayTime = 5;

	private int currentHealth;
	//public GameObject hitParticles;



	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();

		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();
	}

	public void Damage(int damage)
	{
        currentHealth -= damage;
		Debug.Log (currentHealth);
		if (currentHealth <= 0) 
		{
			Defeated ();
		}

	}

	void Defeated()
	{
		Debug.Log ("PlayerDead");
		agent.speed = 0;
		agent.angularSpeed = 0;
		anim.SetBool ("isPlayerDead", true);

		Destroy(gameObject, delayTime);

	}
}
