using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 3;
    private Animator anim;
    public bool isKilled;
    //private NavMeshAgent agent;
    public Material deadMaterial;

    int delayTime = 6;
	public int currentHealth;
	void Start ()
    {
		//agent = GetComponent<NavMeshAgent>();        
        currentHealth = startingHealth;
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
    }
}
