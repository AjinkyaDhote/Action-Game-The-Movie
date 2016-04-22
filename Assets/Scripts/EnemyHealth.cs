using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 3;
    private Animator anim;
    private NavMeshAgent agent;
    public Material deadMaterial;

    int delayTime = 6;

	public int currentHealth;
	//public GameObject hitParticles;



	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
      
        
        currentHealth = startingHealth;
       
        if(gameObject.name == "Spine")
            anim = transform.parent.parent.GetComponent<Animator> ();
        else
            anim = transform.GetComponent<Animator>();
        anim.SetBool("isPlayerDead", false);
    }

	public void Damage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0) 
		{
			Defeated ();
		}
        if (currentHealth < 2)
        {
            foreach (Transform child in this.transform.parent.transform.parent)
            {
                if (child.name == "WorldWar_zombie")
                {
                    child.GetComponent<Renderer>().material = deadMaterial;
                }

            }


        }

	}

	void Defeated()
	{
        Debug.Log("Enemy dead");
        Debug.Log(anim);
        if (!(gameObject.name == "Spine"))
            agent.speed = 0;

            anim.SetBool("isPlayerDead", true);
        //animS.SetBool("isPlayerDead", true);


        //Play animation dying animation;
        if (!(gameObject.name == "Spine"))
            Destroy(gameObject, delayTime);
        else
            Destroy(transform.parent.parent.gameObject, delayTime);

    }
}
