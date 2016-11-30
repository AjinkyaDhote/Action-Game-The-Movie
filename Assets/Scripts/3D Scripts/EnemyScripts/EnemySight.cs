using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public bool isPlayerSeen;
    private Camera enemyCamera;
    //RaycastHit[] hit;
    float rayMaxDistance;
    //Ray[] ray;
    RaycastHit hit;
    Ray ray;
    int rayCount;
    AI_movement aiMovementScript;
    // Use this for initialization
    void Start()
    {
        isPlayerSeen = false;
        //ray = new Ray[8];
        //hit = new RaycastHit[4];
        rayCount = 4;
        rayMaxDistance = 100;

        enemyCamera = GetComponentInChildren<Camera>();
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
    }

    

    // Update is called once per frame
    void Update()
    {
        //ray[0] = enemyCamera.ViewportPointToRay(new Vector3(0, 0.5f, 0));
        //ray[1] = enemyCamera.ViewportPointToRay(new Vector3(0.2f, 0.5f, 0));
        //ray[2] = enemyCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //ray[3] = enemyCamera.ViewportPointToRay(new Vector3(0.7f, 0.5f, 0));

        for(int i=0; i < rayCount; i++)
        {
            ray = enemyCamera.ViewportPointToRay(new Vector3(i * (1 / (float)rayCount), 0.5f, 0));
            
            if (Physics.Raycast(ray, out hit, rayMaxDistance))
            {
                //Debug.Log(hit.collider.name);

                Debug.DrawRay(ray.origin, ray.direction * (hit.distance));
                if ((hit.collider.tag == "Player") || (hit.collider.tag == "NewPayload"))
                {
                    if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
                    {
                        aiMovementScript.Detection(hit.transform);
                    }
                }
            }
        }

        //if (Physics.Raycast(ray[0], out hit[0], rayMaxDistance))
        //{
        //    if (hit[0].collider.tag == "Player")
        //        if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
        //        {
        //            aiMovementScript.Detection(player.transform);
        //        }

        //}
        //if (Physics.Raycast(ray[1], out hit[1], rayMaxDistance))
        //{
        //    if (hit[1].collider.tag == "Player")
        //        if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
        //        {
        //            aiMovementScript.Detection(player.transform);
        //        }

        //}
        //if (Physics.Raycast(ray[2], out hit[2], rayMaxDistance))
        //{
        //    if (hit[2].collider.tag == "Player")
        //        if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
        //        {
        //            aiMovementScript.Detection(player.transform);
        //        }

        //}
        //if (Physics.Raycast(ray[3], out hit[3], rayMaxDistance))
        //{
        //    if (hit[3].collider.tag == "Player")
        //        if (aiMovementScript != null)// && !aiMovementScript.isChasingPayload)
        //        {
        //            aiMovementScript.Detection(player.transform);
        //        }

        //}
    }
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    //Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
    //    Gizmos.DrawRay(ray[0]);
    //}
}
