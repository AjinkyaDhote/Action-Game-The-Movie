using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	
    private Animator anim;
    public bool isKilled;
    //private NavMeshAgent agent;
    private Material deadMaterial;
	private AudioClip ZombieDeath;

    int delayTime;
	public int currentHealth;
	void Start ()
    {
        deadMaterial = Resources.Load("Materials/deadMaterial") as Material;
        ZombieDeath = Resources.Load("Sounds/ZombieDeath") as AudioClip;

        delayTime = 6;
        //agent = GetComponent<NavMeshAgent>();        
        if (transform.parent.parent.CompareTag("SmallEnemy"))
            currentHealth = 10;
        else
            currentHealth = 60;
        anim = transform.parent.parent.GetComponent<Animator> ();
        anim.SetBool("isPlayerDead", false);
        isKilled = false;
        //GameManager.Instance.totalEnemiesKilled = 0;
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
        if (!isKilled)
        {
            Debug.Log("Killed");
            isKilled = true;
            GameManager.Instance.totalEnemiesKilled++;
            anim.SetBool("isPlayerDead", true);
            Destroy(transform.parent.parent.gameObject, delayTime);
        }
		AudioSource.PlayClipAtPoint (ZombieDeath, new Vector3 (transform.position.x, transform.position.y, transform.position.z));
        anim.SetBool("isPlayerDead", true);
        Destroy(transform.parent.parent.gameObject, delayTime);
    }
}
