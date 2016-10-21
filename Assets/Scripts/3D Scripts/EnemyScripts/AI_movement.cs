using UnityEngine;
using System.Collections;

public class AI_movement : MonoBehaviour
{
    public float enemyWalkSpeed;
    public float enemyRunSpeed;
    GameObject player;
    PlayerHealthScript playerHealth;
    GameObject hitRadialPrefab;
    GameObject hitRadial;
    NavMeshAgent agent;
    bool isPlayerInRange;
    Vector3 resetPositionForInRange;
    Animator anim;
    Vector3 initialPos;
    Vector3[] randomVectors;
    Collider enemyBodyCollider, playerCollider, enemyHeadCollider;
    bool _isPlayerSeen = false;
    GameObject arrow_sprite;
    Renderer arrow_renderer;
    //Camera mainCamera;
    Material glitchMaterial;

    public bool IsPlayerSeen
    {
        get
        {
            return _isPlayerSeen;
        }
        set
        {
            _isPlayerSeen = false;
        }
    }


    void Start()
    {
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
        isPlayerInRange = false;
        _isPlayerSeen = false;


        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>();		
        enemyHeadCollider = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Collider>();
        enemyBodyCollider = transform.GetChild(0).GetChild(2).GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealthScript>();
        playerCollider = player.GetComponent<Collider>();
        agent.speed = enemyWalkSpeed;
        arrow_sprite = transform.FindChild("arrow_detection").gameObject;
        arrow_renderer = arrow_sprite.GetComponent<Renderer>();
        arrow_renderer.enabled = false;
        

        Patrol();
        //mainCamera = Camera.main;
        glitchMaterial = Camera.main.GetComponent<ScreenGlitch>().glitchMaterial;
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
    void Update()
    {
        if (anim.GetBool("isPlayerDead") == true)
        {

            agent.speed = 0;
            arrow_renderer.enabled = false;
        }
        else
        {
            //Physics.IgnoreCollision(enemyBodyCollider, playerCollider);
            //Physics.IgnoreCollision(enemyHeadCollider, playerCollider);

            if (IsPlayerSeen)
            {
                transform.LookAt(player.transform);
                if (anim.GetBool("isPlayerInRange") == true)
                {
                    agent.speed = 0;

                    anim.SetBool("isPunch2", true);
                    anim.SetBool("isPunch1", true);
                    transform.position = resetPositionForInRange;
                    transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
                    //if (Vector2.Distance(player.transform.position, transform.position) < 2.0f)
                    //{

                    //    //DamagePlayer(10);
                    //}

                }
                else
                {
                    agent.speed = enemyRunSpeed;
                    anim.SetBool("isPlayerInRange", false);
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
    void DamagePlayer(int damage)
    {
        glitchMaterial.SetFloat("_Magnitude", 0.04f);
        playerHealth.PlayerDamage(damage);
        hitRadial = Instantiate(hitRadialPrefab);
        hitRadial.transform.SetParent(player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas"));
        hitRadial.GetComponent<HitRadial>().StartRotation(transform);
        Destroy(hitRadial, 2.0f);
        StartCoroutine(SetGlitch());        
    }

    public void Detection()
    {
        transform.LookAt(player.transform);
        _isPlayerSeen = true;
        anim.SetBool("isPlayerSeen", true);

        arrow_renderer.enabled = true;
    }
    public void InRange()
    {
        _isPlayerSeen = true;
        anim.SetBool("isPlayerSeen", true);
        resetPositionForInRange = transform.position;
        anim.SetBool("isPlayerInRange", true);
        isPlayerInRange = true;

    }
    public void OutOfRange()
    {
        isPlayerInRange = false;
        anim.SetBool("isPlayerSeen", true);
        anim.SetBool("isPlayerInRange", false);
    }
    void Patrol()
    {
        anim.SetBool("isPlayerSeen", false);
        if (anim.GetBool("isPlayerDead") == true)
        {
            agent.speed = 0;
        }
        agent.destination = GetRandomVector();
    }

    IEnumerator SetGlitch()
    {        
        yield return new WaitForSeconds(1.5f);
        glitchMaterial.SetFloat("_Magnitude", 0.0f);
    }
}