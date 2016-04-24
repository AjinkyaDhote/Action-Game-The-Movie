using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	
    private Animator anim;
    //private NavMeshAgent agent;
    private Material deadMaterial;
	private AudioClip ZombieDeath;

    int delayTime;
	public int currentHealth;
	void Start ()
    {
        deadMaterial = Resources.Load("Material/deadMaterial") as Material;
        ZombieDeath = Resources.Load("Sounds/ZombieDeath") as AudioClip;

        delayTime = 6;
        //agent = GetComponent<NavMeshAgent>();        
        if (transform.parent.parent.name == "Warzombie_F_Pedroso")
            currentHealth = 10;
        else
            currentHealth = 60;
        anim = transform.parent.parent.GetComponent<Animator> ();
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
		AudioSource.PlayClipAtPoint (ZombieDeath, new Vector3 (transform.position.x, transform.position.y, transform.position.z));
        anim.SetBool("isPlayerDead", true);
        Destroy(transform.parent.parent.gameObject, delayTime);
    }
}
