using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    //public bool isPlayerSeen;
    //private Camera enemyCamera;
    //RaycastHit[] hit;
    float MaxDistance;
    //Ray[] ray;
    RaycastHit hit;
    Ray ray;
    //int rayCountHor;
    //int rayCountVer;
    AI_movement aiMovementScript;
    GameObject player;
    GameObject payload;
    GameObject[] playerList;
    //Renderer playerRenderer;
    //Renderer payloadRenderer;
    // Use this for initialization
    void Start()
    {
        //isPlayerSeen = false;
        //ray = new Ray[8];
        //hit = new RaycastHit[4];
        //rayCountHor = 4;
        //rayCountVer = 4;
        MaxDistance = 20;

        //enemyCamera = GetComponentInChildren<Camera>();
        aiMovementScript = transform.root.GetComponentInChildren<AI_movement>();
        player = GameObject.FindGameObjectWithTag("Player");
        payload = GameObject.FindGameObjectWithTag("NewPayload");
        playerList = new GameObject[] { payload, player };

        //playerRenderer = player.GetComponentInChildren<Renderer>();
        //payloadRenderer = payload.GetComponentInChildren<Renderer>();
    }


    // Update is called once per frame
    void Update()
    {
        foreach (GameObject Target in playerList)
        {
            float angle = Vector3.Angle(Target.transform.position - transform.position, transform.forward);
            if (angle < 45)
            {
                Debug.DrawRay(transform.position, (Target.transform.position - transform.position).normalized * MaxDistance, Color.red);
                if (Physics.Raycast(transform.position, (Target.transform.position - transform.position).normalized, out hit, MaxDistance))
                {
                    //Debug.Log(hit.transform.tag + "--" + Target.tag + " " + (hit.transform.tag == Target.tag));
                    if ((hit.transform.tag == Target.tag))// || (hit.collider.tag == "NewPayload"))
                    {
                        if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
                        {
                            aiMovementScript.Detection(hit.transform);
                        }
                    }
                }
            }
        }

        //    //for (int i = 0; i < rayCountHor; i++)
        //    //{
        //    //    for (int j = 2; j < rayCountVer; j++)
        //    //    {
        //    //        ray = enemyCamera.ViewportPointToRay(new Vector3(i * (1 / (float)rayCountHor), j * (1 / (float)rayCountVer)/*0.5f*/, 0));

        //    //        if (Physics.Raycast(ray, out hit, MaxDistance))
        //    //        {
        //    //            //Debug.Log(hit.collider.name);

        //    //            //Debug.DrawRay(ray.origin, ray.direction * (hit.distance), Color.red);
        //    //            if ((hit.collider.tag == "Player") || (hit.collider.tag == "NewPayload"))
        //    //            {
        //    //                if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
        //    //                {
        //    //                    aiMovementScript.Detection(hit.transform);
        //    //                }
        //    //            }
        //    //        }
        //    //        //Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance);
        //    //    }
        //    //}
        //

    }
}
