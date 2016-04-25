using UnityEngine;
using System.Collections;

public class EnemyThrow : MonoBehaviour
{
    public float enemyRunSpeed;
    [HideInInspector]
    public bool isPlayerSeen;
    bool isPlayerInRange;
    bool isCrateThrown;
    bool roarNotYetPlayed;
    Vector3 resetPositionForInRange;
    Animator anim;
    GameObject player;
    Collider enemyBodyCollider, playerCollider, enemyHeadCollider;
    AudioSource roarSource;
    AudioSource attackSource;
    ThrowCrate throwCrateScript;
    PlayerHealthScript playerHealth;
    GameObject hitRadialPrefab;
    GameObject hitRadial;
    NavMeshAgent agent;
    PauseMenu pauseMenuScript;
    void Start()
    {
        isPlayerSeen = false;
        isPlayerInRange = false;
        isCrateThrown = false;
        roarNotYetPlayed = true;
        hitRadialPrefab = Resources.Load("HitRadialPrefab/HitRadial") as GameObject;
        throwCrateScript = transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<ThrowCrate>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyBodyCollider = transform.GetChild(3).GetChild(2).GetComponent<Collider>();
        enemyHeadCollider = transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Collider>();
        playerCollider = player.GetComponent<Collider>();
        playerHealth = player.GetComponent<PlayerHealthScript>();
        roarSource = transform.GetChild(0).GetComponent<AudioSource>();
        attackSource = transform.GetChild(1).GetComponent<AudioSource>();
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
    }

    void Update()
    {
        Physics.IgnoreCollision(enemyBodyCollider, playerCollider);
        Physics.IgnoreCollision(enemyHeadCollider, playerCollider);
        if (isPlayerSeen)
        {
            if(!roarSource.isPlaying && roarNotYetPlayed)
            {
                roarSource.Play();
                roarNotYetPlayed = false;
            }
            if (anim.GetBool("isPlayerDead"))
            {
                agent.speed = 0;
            }
            else
            {
                transform.LookAt(player.transform);
                if (isPlayerInRange)
                {
                    agent.speed = 0;
                    transform.position = resetPositionForInRange;
                    transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
                    if(pauseMenuScript.isPaused)
                    {
                        attackSource.Stop();
                    }
                    else
                    {
                        if (!attackSource.isPlaying)
                        {
                            attackSource.Play();
                        }
                    }                
                }
                else
                {
                    if (isCrateThrown)
                    {
                        agent.speed = enemyRunSpeed;
                        agent.destination = player.transform.position;
                    }
                }  
            }
        }              
    }

    public void Detection()
    {
        transform.LookAt(player.transform);
        isPlayerSeen = true;
        anim.SetBool("isPlayerSeen", true);
    }
    public void InRange()
    {
        anim.SetBool("isPlayerInRange", true);
        resetPositionForInRange = transform.position;
        isPlayerInRange = true;     
    }
    public void OutOfRange()
    {
        anim.SetBool("isPlayerInRange", false);
        isPlayerInRange = false;
    }

    void ThrowCrateNow()
    {
        throwCrateScript.Release();
        isCrateThrown = true;     
    }

    void DamagePlayer()
    {
        playerHealth.PlayerDamage();
        hitRadial = Instantiate(hitRadialPrefab);
        hitRadial.transform.SetParent(player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas"));
        hitRadial.GetComponent<HitRadial>().StartRotation(transform);
        Destroy(hitRadial, 2.0f);
    }
}
