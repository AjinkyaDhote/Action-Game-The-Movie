using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public bool isPlayerSeen;
    private Camera enemyCamera;
    //RaycastHit[] hit;
    float MaxDistance;
    //Ray[] ray;
    RaycastHit hit;
    Ray ray;
    int rayCount;
    AI_movement aiMovementScript;
    GameObject player;
    GameObject payload;
    // Use this for initialization
    void Start()
    {
        isPlayerSeen = false;
        //ray = new Ray[8];
        //hit = new RaycastHit[4];
        rayCount = 4;
        MaxDistance = 20;

        enemyCamera = GetComponentInChildren<Camera>();
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
        player = GameObject.FindGameObjectWithTag("Player");
        payload = GameObject.FindGameObjectWithTag("NewPayload");
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rayCount; i++)
        {
            ray = enemyCamera.ViewportPointToRay(new Vector3(i * (1 / (float)rayCount), 0.5f, 0));

            if (Physics.Raycast(ray, out hit, MaxDistance))
            {
                //Debug.Log(hit.collider.name);

                Debug.DrawRay(ray.origin, ray.direction * (hit.distance),Color.red);
                if ((hit.collider.tag == "Player") || (hit.collider.tag == "NewPayload"))
                {
                    if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
                    {
                        aiMovementScript.Detection(hit.transform);
                    }
                }
            }
            //Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance);
        }

        //----------------------------------------------------------------
        //float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);
        //if(angle < 45)
        //{
        //    //if((player.transform.position - transform.position).sqrMagnitude < (MaxDistance* MaxDistance))
        //    {
        //        if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out hit, MaxDistance))
        //        {

        //        }
        //    }
        //}


    }
}
