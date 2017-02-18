using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{    
    public float droneSpeed;
    public float followSpeed;
    public float hoverSpeed = 10.0f;
    public Vector3 hoverPosition;
    public bool _isPlayer_Payload_Seen = false;
    public bool isPlayerInRange;
    Vector3[] randomVectors;
    Vector3 initialPosition;
    private Transform player;    
    private Transform targetTransform;
    private GameObject sight;

    NavMeshAgent agent;
    private GameObject bulletEmitter;
    private GameObject bulletPrefab;
    private float bulletForce;
    private bool bulletShot;
    private bool engaged;
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        droneSpeed = 3.0f;        
        bulletShot = false;
        engaged = false;
        agent.speed = droneSpeed;
        hoverPosition = transform.position;
        randomVectors = new Vector3[8];
        player = GameObject.Find("FPSPlayer").GetComponent<Transform>();
        //droneChild = GameObject.Find("DroneChild").GetComponent<Transform>();
        randomVectors[0] = new Vector3(1.0f, 1.0f, 0.0f);
        randomVectors[1] = new Vector3(1.0f, 1.0f, 1.0f);
        randomVectors[2] = new Vector3(0.0f, 1.0f, 1.0f);
        randomVectors[3] = new Vector3(-1.0f, 1.0f, 1.0f);
        randomVectors[4] = new Vector3(-1.0f, 1.0f, 0.0f);
        randomVectors[5] = new Vector3(-1.0f, 1.0f, -1.0f);
        randomVectors[6] = new Vector3(0.0f, 1.0f, -1.0f);
        randomVectors[7] = new Vector3(1.0f, 1.0f, -1.0f);
        bulletForce = 200;
        initialPosition = gameObject.transform.position;
        sight = transform.GetChild(0).gameObject;
        //isPlayerSeen = isPlayerOutOfRange =false;
        //Physics.IgnoreCollision(player.GetComponent<Collider>(), droneChild.GetComponent<Collider>());
      
        bulletEmitter = GameObject.Find("Nose");
        bulletPrefab = Resources.Load("Bullet Prefab/Bullet") as GameObject;
        
        Patrol();
    }
	
	
	void Update ()
    {        
        if (_isPlayer_Payload_Seen)
        {
            agent.speed = 0;
            transform.LookAt(player);

            if (!bulletShot)
            {
                bulletShot = true;
                //StartCoroutine(WaitToShoot());
            }

        }
        else if(engaged)
        {
            agent.speed = followSpeed;
            agent.destination = targetTransform.position;
        }

        else if (agent.remainingDistance < 0.5f && !engaged)
        {
            Patrol();
        }
        // //if (Vector3.Distance(targetTransform.position, transform.position) > 15)
        //// {
        //     //isPlayerOutOfRange = true;

        //     //if (isPlayerOutOfRange)
        //     {
        //         //FollowPlayer();
        //        // if (!bulletShot)
        //        // {
        //        //     bulletShot = true;
        //        //     StartCoroutine(WaitToShoot());
        //        // }

        //         //WaitToShoot();
        //     }

        //// }

        //else if (Vector3.Distance(targetTransform.position, transform.position) < 12)
        //{
        //   // isPlayerOutOfRange = false;
        //    //if (!isPlayerOutOfRange && (Vector3.Distance(player.transform.position, transform.position) < 5))
        //    {
        //        Hover();
        //    }
        //}

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
        transform.position.Set(transform.position.x, Mathf.Sin(Time.realtimeSinceStartup * hoverSpeed), transform.position.z); 
    }

    void FollowPlayer()
    {
        //transform.position += player.position * droneSpeed * Time.deltaTime;
        agent.speed = 7;
        agent.destination = player.transform.position;
  
    }


    IEnumerator WaitToShoot()
    {
        Shooting();
        yield return new WaitForSeconds(2);               
    }
    void Shooting()
    {        
    //    bulletPrefab = Instantiate(bulletPrefab, bulletEmitter.transform.position,Quaternion.identity) as GameObject;
       
    //    Rigidbody bulletRB;
    //    bulletRB = bulletPrefab.GetComponent<Rigidbody>();
    //    bulletRB.AddForce(transform.forward * bulletForce);
    //    bulletShot = false;        
    }   

    public void Detection(Transform transformToLookAt)
    {        
        _isPlayer_Payload_Seen = true;
       //agent.destination = transformToLookAt.position;
        //sight.transform.LookAt(transformToLookAt);        
    }

    public void InRange(Transform other)
    {
        isPlayerInRange = true;
    }

    public void OutOfRange(Transform transformToFollow)
    {
        _isPlayer_Payload_Seen = false;
        engaged = true;
        targetTransform = transformToFollow;
    }
}
