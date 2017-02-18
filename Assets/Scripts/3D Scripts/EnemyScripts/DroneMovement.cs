using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{

    public float droneSpeed = 3.0f;
    public float hoverSpeed = 0.0f;
    public Vector3 hoverPosition;
    public bool isPlayer_PayloadSeen;
    public bool isPlayerSeen;
    public bool isPayloadSeen;
    EnemyHealth enemyHealth;
    public bool isPlayerOutOfRange;
    public Rigidbody rb;
    Vector3[] randomVectors;
    Vector3 initialPosition;
    private Transform player;
    private Transform payload;
    private Transform droneChild;
    public Vector3 startHoverPosition;

    NavMeshAgent agent;
    private GameObject bulletEmitter;
    private GameObject bullet;
    private float bulletForce;
    private bool bulletShot;
    GameObject bulletGameObject;
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
       enemyHealth= GetComponent<EnemyHealth>();
        droneSpeed = 3.0f;
        hoverSpeed = 5.0f;
        bulletShot = false;
        agent.speed = droneSpeed;
        hoverPosition = transform.position;
        randomVectors = new Vector3[8];
        player = GameObject.Find("FPSPlayer").GetComponent<Transform>();
        payload = GameObject.Find("PayLoad").GetComponent<Transform>();
        droneChild = GameObject.Find("DroneChild").GetComponent<Transform>();
        randomVectors[0] = new Vector3(1.0f, 1.0f, 0.0f);
        randomVectors[1] = new Vector3(1.0f, 1.0f, 1.0f);
        randomVectors[2] = new Vector3(0.0f, 1.0f, 1.0f);
        randomVectors[3] = new Vector3(-1.0f, 1.0f, 1.0f);
        randomVectors[4] = new Vector3(-1.0f, 1.0f, 0.0f);
        randomVectors[5] = new Vector3(-1.0f, 1.0f, -1.0f);
        randomVectors[6] = new Vector3(0.0f, 1.0f, -1.0f);
        randomVectors[7] = new Vector3(1.0f, 1.0f, -1.0f);
        bulletForce = 500;
        initialPosition = gameObject.transform.position;
        isPlayer_PayloadSeen = isPlayerSeen = isPayloadSeen = isPlayerOutOfRange =false;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), droneChild.GetComponent<Collider>());
        //rb = GetComponent<Rigidbody>();
        //rb.detectCollisions = false;

      
        bulletEmitter = GameObject.Find("Nose");
        bullet = Resources.Load("Bullet Prefab/DroneBullet") as GameObject;
        
        Patrol();
    }
	
	
	void Update ()
    {
        if (!enemyHealth.IsKilled)
        {
            
            Physics.IgnoreCollision(player.transform.GetComponent<Collider>(), transform.GetComponent<Collider>());
            if (agent.remainingDistance < 0.5f)
            {
                Patrol();
            }
            if (isPlayer_PayloadSeen)
            {
                if (isPayloadSeen == true)
                {
                    transform.LookAt(payload);
                }

                else if (isPlayerSeen == true)
                {
                    transform.LookAt(player);
                }


                if (Vector3.Distance(player.transform.position, transform.position) > 6)
                {
                    isPlayerOutOfRange = true;

                    if (isPlayerOutOfRange)
                    {
                        if (isPlayerSeen == true)
                        {
                            FollowPlayer();
                            isPayloadSeen = false;
                        }

                        if (!bulletShot)
                        {
                            bulletShot = true;
                            StartCoroutine(WaitToShoot());
                        }
                    }

                }

                else if (Vector3.Distance(player.transform.position, transform.position) < 10)
                {
                    isPlayerOutOfRange = false;
                    //if (!isPlayerOutOfRange && (Vector3.Distance(player.transform.position, transform.position) < 5))
                    {
                      
                        Hover();
                        if (!bulletShot)
                        {
                            bulletShot = true;
                            StartCoroutine(WaitToShoot());
                        }

                    }

                }

            }
        }
        else
        {
           // if ((transform.position.y > 0.0f))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 70.0f, transform.position.z);
            }

        }


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
            randomIndex = (randomIndex + 1) % 8;
            randomPoint = randomVectors[randomIndex];
            randomPoint *= 4;
            randomPoint += initialPosition;

            direction = randomPoint - gameObject.transform.position;
            hit = Physics.Raycast(gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f), direction.normalized, direction.magnitude);

        } while (hit && initialIndex != randomIndex);

        return randomPoint;
    }

    void Patrol()
    {
        agent.destination = GetRandomVector();
        
    }

    void Hover()
    {
       // Debug.Log(Mathf.Sin(Time.realtimeSinceStartup * hoverSpeed));
        agent.speed = 0;
        Vector3 Hover;
        //hoverSpeed += 0.001f;
       // Debug.Log(startHoverPosition);
       // Hover = new Vector3(startHoverPosition.x + 10 *Mathf.Sin( hoverSpeed*Mathf.Deg2Rad), startHoverPosition.y + 10* Mathf.Sin(hoverSpeed * Mathf.Deg2Rad) * Mathf.Cos(hoverSpeed * Mathf.Deg2Rad));
        Hover = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.realtimeSinceStartup * hoverSpeed), transform.position.z);
        transform.position = Hover;
    }

    void FollowPlayer()
    {
        //transform.position += player.position * droneSpeed * Time.deltaTime;
        agent.speed = 7;
        agent.destination = player.transform.position;
    }


    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(2);
       Shooting();
    }
    void Shooting()
    {
        GameObject bulletGameObject;
        bulletGameObject = Instantiate(bullet, bulletEmitter.transform.position,Quaternion.identity) as GameObject;
        Rigidbody bulletRB;
        bulletRB = bulletGameObject.GetComponent<Rigidbody>();
        bulletRB.AddForce(transform.forward * bulletForce);
        bulletShot = false;

    }       
}
