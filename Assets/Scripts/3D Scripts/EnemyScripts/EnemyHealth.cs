using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private Transform playerTransform;
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
    private NavMeshAgent agent;
    public int currentHealth;
    //AI_movement aiMovementScript;
    
    void Start()
    {
        //aiMovementScript = transform.GetComponentInParent<AI_movement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();        
        if (transform.CompareTag("SmallEnemy"))
            currentHealth = 5;
        else
            currentHealth = 60;
        anim = transform.GetComponent<Animator>();
        //isPlayerDead = false;
        _isKilled = false;
        GameManager.Instance.totalEnemiesKilled = 0;
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
            anim.SetBool("isPunch1", false);
            anim.SetBool("isEnemyDead", true);
            transform.GetChild(1).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
            transform.GetChild(1).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
            // enemyHead.HeadFall();
            //gameObject.GetComponent<Renderer>().material.SetColor("spec", colorDead);

            GameManager.Instance.totalEnemiesKilled++;
        }
        //AudioSource.PlayClipAtPoint(ZombieDeath, new Vector3(transform.position.x, transform.position.y, transform.position.z));

        Destroy(gameObject, 5.0f);
    }
}
