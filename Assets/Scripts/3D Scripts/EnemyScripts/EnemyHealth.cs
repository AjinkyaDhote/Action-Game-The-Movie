using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	
    //private Animator anim;
    public bool isKilled;
    private bool isPlayerDead;
    //private NavMeshAgent agent;
    private Material deadMaterial;
	private AudioClip ZombieDeath;
    private Color colorDead;
    private Renderer rend;

    int delayTime;
	public int currentHealth;
	void Start ()
    {
        deadMaterial = Resources.Load("Materials/deadMaterial") as Material;
        ZombieDeath = Resources.Load("Sounds/ZombieDeath") as AudioClip;
        colorDead = Color.red;
        delayTime = 6;
        //agent = GetComponent<NavMeshAgent>();        
        if (transform.CompareTag("SmallEnemy"))
            currentHealth = 10;
        else
            currentHealth = 60;
        //anim = transform.parent.parent.GetComponent<Animator> ();
        isPlayerDead = false;
        isKilled = false;
        //GameManager.Instance.totalEnemiesKilled = 0;
    }

	public void Damage(int damage)
	{
		currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Defeated();



            foreach (Transform child in this.transform.parent.transform.parent)
            {
                if (child.tag == "SmallEnemy")
                {
                    child.GetComponent<Renderer>().material.color = colorDead;
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
            isPlayerDead = true;
            gameObject.GetComponent<Renderer>().material.SetColor("spec",colorDead);
            Destroy(transform.gameObject, delayTime);
            int a = 0;
        }
		AudioSource.PlayClipAtPoint (ZombieDeath, new Vector3 (transform.position.x, transform.position.y, transform.position.z));
        isPlayerDead = true;
        Destroy(transform.parent.parent.gameObject, delayTime);
    }
}
