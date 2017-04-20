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
    UnityEngine.AI.NavMeshAgent agent;
    private bool hasPlayed;
    //bool isPlayerInRange;
    //Vector3 resetPositionForInRange;
    [HideInInspector]
    public Animator anim;
    Vector3 initialPos;
    Vector3[] randomVectors;
    Collider enemyBodyCollider, enemyHeadCollider;
    bool _isPlayer_Payload_Seen = false;
    GameObject arrow_sprite;
    Renderer arrow_renderer;
    //Camera mainCamera;
    //[HideInInspector]
    //public bool isChasingPayload = false;
    //[HideInInspector]
    //public bool isChasingPlayer = false;
    GameObject payload;
    EnemyHealth enemyHealth;
    //RaycastHit raycastHit;
    //[HideInInspector]
    //public Transform enemyRayCastHelper;
    //Transform payloadRayCastHelper;

    Transform targetTransform;
    [HideInInspector]
    public bool engaged;
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
        hasPlayed = false;
        //enemyRayCastHelper = transform.FindChild("RaycastHelper").transform;
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


        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //anim = GetComponent<Animator>();		
        enemyHeadCollider = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Collider>();
        enemyBodyCollider = transform.GetChild(0).GetChild(2).GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealthScript>();
        payload = GameObject.FindGameObjectWithTag("NewPayload");
        //payloadRayCastHelper = payload.transform.FindChild("RaycastHelper").transform;
        payLoadHealthScript = payload.transform.GetChild(5).GetComponent<PayLoadHealthScript>();
        agent.speed = enemyWalkSpeed;
        arrow_sprite = transform.FindChild("arrow_detection").gameObject;
        arrow_renderer = arrow_sprite.GetComponent<Renderer>();
        arrow_renderer.enabled = false;

        engaged = false;        

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

        } while (hit && initialIndex != randomIndex);

        return randomPoint;
    }
    void Update()
    {
        if (!enemyHealth.IsKilled)
        {
            if (_isPlayer_Payload_Seen)
            {
                //transform.LookAt(targetTransform);
                if (anim.GetBool("isPlayer_PayloadInRange") && engaged)
                {
                    agent.speed = 0;
                    anim.SetBool("isPunch1", true);
                    //transform.position = resetPositionForInRange;
                }
                else
                {
                    agent.speed = enemyRunSpeed;
                    anim.SetBool("isPlayer_PayloadInRange", false);
                    anim.SetBool("isPunch1", false);
                    agent.destination = targetTransform.position;
                }
                transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
            }
            else
            {
                if (agent.remainingDistance < 0.5f)
                {
                    Patrol();
                }
            }
        }
        else
        {
            enemyBodyCollider.enabled = false;
            enemyHeadCollider.enabled = false;
            arrow_renderer.enabled = false;
            agent.speed = 0;
        }
    }
    void DamagePlayer(int damage)
    {
        if (targetTransform.CompareTag("Player"))
        {
            playerHealth.PlayerDamage(damage, 0.07f, gameObject.name);

            hitRadial = Instantiate(hitRadialPrefab);
            hitRadial.transform.SetParent(player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas"));
            hitRadial.GetComponent<HitRadial>().StartRotation(transform);
            Destroy(hitRadial, 2.0f);
        }
        else if (targetTransform.CompareTag("NewPayload"))
        {
            payLoadHealthScript.PayLoadDamage(gameObject.tag);
        }
    }

    public void Detection(Transform transformToLookAt)
    {
        if (!hasPlayed)
        {
            SoundManager3D.Instance.intruderAlert.Play();
            hasPlayed = true;
        }
        //transform.LookAt(transformToLookAt);
        _isPlayer_Payload_Seen = true;
        anim.SetBool("isPlayer_PayloadSeen", true);
        arrow_renderer.enabled = true;

        targetTransform = transformToLookAt;
    }
    public void InRange(Transform transformToLookAt)
    {
        engaged = true;

        transform.LookAt(transformToLookAt);
        //resetPositionForInRange = transform.position;
        anim.SetBool("isPlayer_PayloadInRange", true);

        targetTransform = transformToLookAt;
        if (targetTransform.CompareTag("NewPayload"))
        {
            payLoadHealthScript.StopPayload();
        }
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