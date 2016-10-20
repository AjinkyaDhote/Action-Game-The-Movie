using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private bool _isKilled = false;


    public bool IsKilled
    {
        get
        {
            return _isKilled;
        }
    }
    private bool isPlayerDead;
    //private NavMeshAgent agent;
    private Material deadMaterial;
   


    public int currentHealth;
    AI_movement aiMovementScript;
    
    void Start()
    {
        deadMaterial = Resources.Load("Materials/deadMaterial") as Material;
      
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
        
        //agent = GetComponent<NavMeshAgent>();        
        if (transform.CompareTag("SmallEnemy"))
            currentHealth = 5;
        else
            currentHealth = 60;
        anim = transform.GetComponentInParent<Animator>();
        isPlayerDead = false;
        _isKilled = false;
        //GameManager.Instance.totalEnemiesKilled = 0;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        
        if (aiMovementScript != null)
        {
            aiMovementScript.Detection();
        }
        if (currentHealth <= 0)
        {
            Defeated();
           
        }

    }

    void Defeated()
    {
        if (!_isKilled)
        {
            Debug.Log("Killed");
            _isKilled = true;
            anim.SetBool("isPunch1", false);
            anim.SetBool("isPlayerDead", true);
           
           // enemyHead.HeadFall();
            //gameObject.GetComponent<Renderer>().material.SetColor("spec", colorDead);
           
            //GameManager.Instance.totalEnemiesKilled++;
        }
        //AudioSource.PlayClipAtPoint(ZombieDeath, new Vector3(transform.position.x, transform.position.y, transform.position.z));

        Destroy(gameObject, 5.0f);
    }
}
