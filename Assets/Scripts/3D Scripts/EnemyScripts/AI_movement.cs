using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour
{
    public float enemyWalkSpeed;
    public float enemyRunSpeed;
    GameObject player;
    PlayerHealthScript playerHealth;
    PayLoadHealthScript payLoadHealthScript;
    GameObject hitRadialPrefab;
    GameObject hitRadial;
    NavMeshAgent agent;
    //bool isPlayerInRange;
    Vector3 resetPositionForInRange;
    [HideInInspector]
    public Animator anim;
    Vector3 initialPos;
    Vector3[] randomVectors;
    Collider enemyBodyCollider, playerCollider, enemyHeadCollider;
    bool _isPlayer_Payload_Seen = false;
    GameObject arrow_sprite;
    Renderer arrow_renderer;
    //Camera mainCamera;
    [HideInInspector]
    public bool isChasingPayload = false;
    [HideInInspector]
    public bool isChasingPlayer = false;
    GameObject payload;
    EnemyHealth enemyHealth;
    AudioSource detectionSound;
    static int isSoundNecessary = 0;
    AudioSource enemyHit;
    AudioSource playerHit;
    //RaycastHit raycastHit;
    //[HideInInspector]
    //public Transform enemyRayCastHelper;
    //Transform payloadRayCastHelper;
    public bool IsPlayerPayloadSeen
    {
        get
        {
            return _isPlayer_Payload_Seen;
        }
        set
        {
            _isPlayer_Payload_Seen = value;
        }
    }


    void Start()
    {
        //enemyRayCastHelper = transform.FindChild("RaycastHelper").transform;
        detectionSound = GetComponent<AudioSource>();
        enemyHit = transform.GetChild(0).GetComponent<AudioSource>();
        enemyHealth = GetComponent<EnemyHealth>();
        randomVectors = new Vector3[8];

        randomVectors[0] = new Vector3(1.0f, 1.0f, 0.0f);
        randomVectors[1] = new Vector3(1.0f, 1.0f, 1.0f);
        randomVectors[2] = new Vector3(0.0f, 1.0f, 1.0f);
        randomVectors[3] = new Vector3(-1.0f, 1.0f, 1.0f);
        randomVectors[4] = new Vector3(-1.0f, 1.0f, 0.0f);
        randomVectors[5] = new Vector3(-1.0f, 1.0f, -1.0f);
        randomVectors[6] = new Vector3(0.0f, 1.0f, -1.0f);
        randomVectors[7] = new Vector3(1.0f, 1.0f, -1.0f);

        hitRadialPrefab = Resources.Load("HitRadialPrefab/HitRadial") as GameObject;
        Random.InitState(42);
        initialPos = gameObject.transform.position;



        anim = GetComponent<Animator>();
        //isPlayerInRange = false;
        IsPlayerPayloadSeen = false;


        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>();		
        enemyHeadCollider = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Collider>();
        enemyBodyCollider = transform.GetChild(0).GetChild(2).GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealthScript>();
        playerCollider = player.GetComponent<Collider>();
        playerHit = player.GetComponent<AudioSource>();
        payload = GameObject.FindGameObjectWithTag("NewPayload");
        //payloadRayCastHelper = payload.transform.FindChild("RaycastHelper").transform;
        payLoadHealthScript = payload.transform.GetChild(5).GetComponent<PayLoadHealthScript>();
        agent.speed = enemyWalkSpeed;
        arrow_sprite = transform.FindChild("arrow_detection").gameObject;
        arrow_renderer = arrow_sprite.GetComponent<Renderer>();
        arrow_renderer.enabled = false;
        

        Patrol();
        //mainCamera = Camera.main;
    }

    Vector3 GetRandomVector()
    {
        bool hit = true;
        Vector3 randomPoint;
        Vector3 direction;
        int randomIndex = Random.Range(0, 8);
        int initialIndex = randomIndex;

        do
        {
            //randomPoint.x = (Random.value * (radius - minRadius)) + minRadius;
            //randomPoint.y = 1.0f;
            //randomPoint.z = (Random.value * (radius - minRadius)) + minRadius;

            randomIndex = (randomIndex + 1) % 8;
            randomPoint = randomVectors[randomIndex];
            randomPoint *= 4;
            randomPoint += initialPos;

            direction = randomPoint - gameObject.transform.position;// + new Vector3(0.0f, 1.0f, 0.0f);
            hit = Physics.Raycast(gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), direction.normalized, direction.magnitude);
            //Debug.DrawRay(gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), direction.normalized, Color.red, 1);

        } while (hit && initialIndex  != randomIndex);

        return randomPoint;
    }

    public void PlayEnemyHitSound()
    {
        if (!enemyHealth.IsKilled)
        {
            enemyHit.Play();
        }
    }
    void Update()
    {
        if (anim.GetBool("isEnemyDead"))
        {
            arrow_renderer.enabled = false;
        }
        //------------------------------------------------Player---------------------------------------------
        if (!isChasingPayload && (!enemyHealth.IsKilled))
        {
                if (anim.GetBool("isEnemyDead"))
            {
                agent.speed = 0;
                arrow_renderer.enabled = false;
            }
            else
            {
                //Physics.IgnoreCollision(enemyBodyCollider, playerCollider);
                //Physics.IgnoreCollision(enemyHeadCollider, playerCollider);

                if (_isPlayer_Payload_Seen)
                {
                    transform.LookAt(player.transform);              
                    if (anim.GetBool("isPlayer_PayloadInRange") && isChasingPlayer)
                    {
                        agent.speed = 0;
                        anim.SetBool("isPunch2", true);
                        anim.SetBool("isPunch1", true);
                        transform.position = resetPositionForInRange;
                        transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
                    }
                    else
                    {
                        isChasingPlayer = true;
                        agent.speed = enemyRunSpeed;
                        anim.SetBool("isPlayer_PayloadInRange", false);
                        anim.SetBool("isPunch1", false);
                        transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
                        agent.destination = player.transform.position;
                    }
                }
                else
                {
                    if (agent.remainingDistance < 0.5f)
                    {
                        Patrol();
                    }
                }
            }
        }
        //------------------------------------------------Player---------------------------------------------

        //------------------------------------------------Payload---------------------------------------------
        if (!isChasingPlayer && (!enemyHealth.IsKilled))
        {
            if (!anim.GetBool("isPlayer_PayloadSeen") && Vector3.Distance(transform.position, payload.transform.position) < 20.0f)
            {
                Detection(payload.transform);
                isChasingPayload = true;
            }
            if (anim.GetBool("isPlayer_PayloadSeen") && !anim.GetBool("isPlayer_PayloadInRange"))
            {
                agent.destination = payload.transform.position;
                transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
                agent.speed = enemyRunSpeed;
            }
            if (Vector3.Distance(transform.position, payload.transform.position) < 10.0f)
            {
                //Physics.Raycast(enemyRayCastHelper.position, (payloadRayCastHelper.position - enemyRayCastHelper.position).normalized, out raycastHit, Mathf.Infinity);
                
                //if (raycastHit.collider.gameObject.tag == "NewPayload")
                {
                    InRange(payload.transform);
                    agent.speed = 0;
                    transform.position = resetPositionForInRange;
                    transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
                    anim.SetBool("isPunch1", true);
                }
            }
            else if (anim.GetBool("isPlayer_PayloadInRange") && Vector3.Distance(player.transform.position, payload.transform.position) >= 10.0f)
            {
                OutOfRange();
            }
        }
        //------------------------------------------------Payload---------------------------------------------
    }
    void DamagePlayer(int damage)
    {
        if (isChasingPlayer)
        {
            playerHit.Play();
            playerHealth.PlayerDamage(damage, 0.08f, gameObject.name);
            hitRadial = Instantiate(hitRadialPrefab);
            hitRadial.transform.SetParent(player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas"));
            hitRadial.GetComponent<HitRadial>().StartRotation(transform);
            Destroy(hitRadial, 2.0f);
        } 
    }

    void DamagePayload()
    {
        if(isChasingPayload)
        {
            payLoadHealthScript.PayLoadDamage();
        }
    }


    public void Detection(Transform transformToLookAt, bool isSoundToBePlayed = true)
    {
        if ((isSoundNecessary % 2 == 0) && isSoundToBePlayed)
        {
            detectionSound.Play();
        }
        isSoundNecessary++;
        transform.LookAt(transformToLookAt);
        _isPlayer_Payload_Seen = true;
        anim.SetBool("isPlayer_PayloadSeen", true);
        arrow_renderer.enabled = true;
    }
    public void InRange(Transform transformToLookAt)
    {
        transform.LookAt(transformToLookAt);
        resetPositionForInRange = transform.position;
        anim.SetBool("isPlayer_PayloadInRange", true);
    }
    public void OutOfRange()
    {
        anim.SetBool("isPlayer_PayloadInRange", false);
    }
    void Patrol()
    {
        anim.SetBool("isPlayer_PayloadSeen", false);
        if (anim.GetBool("isEnemyDead") == true)
        {
            agent.speed = 0;
        }
        agent.destination = GetRandomVector();
    }
}