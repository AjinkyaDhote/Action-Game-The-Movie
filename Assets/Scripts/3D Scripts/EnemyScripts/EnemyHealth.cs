using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private Transform playerTransform;
    
    //private DroneMovement droneMovementScript;
    [SerializeField]
    private bool _isKilled = false;
    public bool IsKilled
    {
        get
        {
            return _isKilled;
        }
    }
    //private bool isPlayerDead;
    private UnityEngine.AI.NavMeshAgent agent;
    public int currentHealth;
    //AI_movement aiMovementScript;
    
    void Start()
    {
        //droneMovementScript = GameObject.Find transform.GetComponentInParent<DroneMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();        
        if (transform.CompareTag("SmallEnemy"))
            currentHealth = 5;
        else if (transform.CompareTag("DroneEnemy"))
            currentHealth = 3;
        anim = transform.GetComponent<Animator>();
        //isPlayerDead = false;
        _isKilled = false;
        //GameManager.Instance.totalEnemiesKilled = 0;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        transform.LookAt(playerTransform);
        if (currentHealth <= 0)
        {
            agent.speed = 0.0f;
            Defeated();           
        }

    }

    void Defeated()
    {
        if (!_isKilled)
        {
            if (currentHealth < -900)
            {
                GameManager.Instance.headShots++;
            }
            SoundManager3D.Instance.enemyDeath.Play();
            //Debug.Log("Killed");
            _isKilled = true;
            if(gameObject.tag == "SmallEnemy")
            {
                anim.SetBool("isPunch1", false);
                anim.SetBool("isEnemyDead", true);
                transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
            }
            if (gameObject.tag == "DroneEnemy")
            {                                
                Destroy(gameObject, 0.05f);
            }

                // enemyHead.HeadFall();
                //gameObject.GetComponent<Renderer>().material.SetColor("spec", colorDead);

                GameManager.Instance.totalEnemiesKilled++;
        }
        //AudioSource.PlayClipAtPoint(ZombieDeath, new Vector3(transform.position.x, transform.position.y, transform.position.z));

        Destroy(gameObject, 5.0f);
    }
}
