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
    int rayCountHor;
    int rayCountVer;
    AI_movement aiMovementScript;
    GameObject player;
    GameObject payload;
    //Renderer playerRenderer;
    Renderer payloadRenderer;
    Renderer enemyRenderer;
    // Use this for initialization
    void Start()
    {
        isPlayerSeen = false;
        //ray = new Ray[8];
        //hit = new RaycastHit[4];
        rayCountHor = 4;
        rayCountVer = 4;
        MaxDistance = 20;

        enemyCamera = GetComponentInChildren<Camera>();
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
        player = GameObject.FindGameObjectWithTag("Player");
        payload = GameObject.FindGameObjectWithTag("NewPayload");
        //playerRenderer = player.GetComponentInChildren<Renderer>();
        payloadRenderer = payload.GetComponentInChildren<Renderer>();
        enemyRenderer = GetComponentInChildren<Renderer>();
    }


    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < rayCount; i++)
        //{
        //    ray = enemyCamera.ViewportPointToRay(new Vector3(i * (1 / (float)rayCount), 0.5f, 0));

        //    if (Physics.Raycast(ray, out hit, MaxDistance))
        //    {
        //        //Debug.Log(hit.collider.name);

        //        Debug.DrawRay(ray.origin, ray.direction * (hit.distance),Color.red);
        //        if ((hit.collider.tag == "Player") || (hit.collider.tag == "NewPayload"))
        //        {
        //            if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
        //            {
        //                aiMovementScript.Detection(hit.transform);
        //            }
        //        }
        //    }
        //    //Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance);
        //}

        //----------------------------------------------------------------
        float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);
        if (angle < 45)
        {
            //Debug.DrawRay(transform.position + (enemyRenderer.bounds.size.y * Vector3.up *0.8f)/*GetComponentInChildren<Renderer>().bounds.center*/, (player.transform.position - (transform.position + (enemyRenderer.bounds.size.y * Vector3.up * 0.8f))).normalized * MaxDistance);
            //if (Physics.Raycast(transform.position, (player.transform.position - (transform.position + (enemyRenderer.bounds.size.y * Vector3.up * 0.8f))).normalized, out hit, MaxDistance))
            //{
            //    if ((hit.collider.tag == "Player"))// || (hit.collider.tag == "NewPayload"))
            //    {
            //        if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
            //        {
            //            aiMovementScript.Detection(hit.transform);
            //        }
            //    }
            //}

            for (int i = 0; i < rayCountHor; i++)
            {
                for (int j = 0; j < rayCountVer; j++)
                {
                    ray = enemyCamera.ViewportPointToRay(new Vector3(i * (1 / (float)rayCountHor), j * (1 / (float)rayCountVer)/*0.5f*/, 0));

                    if (Physics.Raycast(ray, out hit, MaxDistance))
                    {
                        //Debug.Log(hit.collider.name);

                        //Debug.DrawRay(ray.origin, ray.direction * (hit.distance), Color.red);
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
            }
        }


    }
}
