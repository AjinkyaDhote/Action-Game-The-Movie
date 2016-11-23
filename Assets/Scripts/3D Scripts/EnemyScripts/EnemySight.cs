//using UnityEngine;
//using System.Collections;

//public class EnemySight : MonoBehaviour
//{
//    public bool isPlayerSeen;
//    private Camera enemyCamera;
//    RaycastHit[] hit;
//    Transform player;
//    float rayMaxDistance;
//    Ray[] ray;
//    AI_movement aiMovementScript;
//    // Use this for initialization
//    void Start()
//    {
//        isPlayerSeen = false;
//        enemyCamera = GetComponent<Camera>();
//        ray = new Ray[8];
//        hit = new RaycastHit[4];
//        rayMaxDistance = Mathf.Infinity;
//        aiMovementScript = transform.GetComponentInParent<AI_movement>();
//        player = GameObject.Find("SmallEnemy").GetComponent<Transform>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        ray[0] = enemyCamera.ViewportPointToRay(new Vector3(0, 1, 0));
//        ray[1] = enemyCamera.ViewportPointToRay(new Vector3(1, 1, 0));
//        ray[2] = enemyCamera.ViewportPointToRay(new Vector3(0, 0, 0));
//        ray[3] = enemyCamera.ViewportPointToRay(new Vector3(1, 0, 0));

      

//        if (Physics.Raycast(ray[0], out hit[0], rayMaxDistance)) 
//        {
//            if(hit[0].collider.tag == "Player")
//            if (aiMovementScript != null && !aiMovementScript.isChasingPayload)
//            {
//                aiMovementScript.Detection(player.transform);
//            }

//        }
//        if (Physics.Raycast(ray[1], out hit[1], rayMaxDistance))
//        {
//            if (hit[1].collider.tag == "Player")
//                if (aiMovementScript != null && !aiMovementScript.isChasingPayload)
//            {
//                aiMovementScript.Detection(player.transform);
//            }

//        }
//        if (Physics.Raycast(ray[2], out hit[2], rayMaxDistance))
//        {
//            if (hit[2].collider.tag == "Player")
//                if (aiMovementScript != null && !aiMovementScript.isChasingPayload)
//            {
//                aiMovementScript.Detection(player.transform);
//            }

//        }
//        if (Physics.Raycast(ray[3], out hit[3], rayMaxDistance))
//        {
//            if (hit[3].collider.tag == "Player")
//                if (aiMovementScript != null && !aiMovementScript.isChasingPayload)
//            {
//                aiMovementScript.Detection(player.transform);
//            }

//        }





//    }
//}
