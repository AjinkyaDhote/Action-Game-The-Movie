using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PayLoadMovementScript : MonoBehaviour
{
    public int payLoadSpeed;
    Vector3[] wayPoints3D;
    
    int wayPointNumber;
    
    [HideInInspector]
    public static CountdownTimerScript countdownTimer;
    
    private bool lastReached;    
    private bool isRotating;


    float width2DPlane, width3DPlane, height2DPlane, height3DPlane;

    Vector3 convertPoint(Vector2 relativePoint)
    {
        Vector3 returnVal;
        returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
        returnVal.y = transform.position.y;
        returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
        return returnVal;
    }

    void Start()
    {        
        
        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();

        width2DPlane = GameManager.Instance.width2DPlane;
        height2DPlane = GameManager.Instance.height2DPlane;
        width3DPlane = GameManager.Instance.width3DPlane;
        height3DPlane = GameManager.Instance.height3DPlane;

        wayPointNumber = 1;


        if (GameManager.Instance.mapPoints.Count == 1)
        {
            wayPointNumber = 0;
            lastReached = true;
        }
        else
        {
            lastReached = false;
        }
        wayPoints3D = new Vector3[GameManager.Instance.mapPoints.Count];

        for (int i = 0; i < wayPoints3D.Length; i++)
        {
            wayPoints3D[i] = convertPoint(GameManager.Instance.mapPoints[i]);
        }
        transform.position = wayPoints3D[0];
        transform.LookAt(wayPoints3D[wayPointNumber]);
        
    }
  

    void Update()
    {        
        if (Vector3.Distance(transform.position, wayPoints3D[wayPointNumber]) < 0.5f)
        {
            isRotating = true;
            if (lastReached == false && wayPointNumber == wayPoints3D.Length - 1)
            {
                lastReached = true;
            }

            if (wayPointNumber < (wayPoints3D.Length - 1))
            {
                wayPointNumber++;
            }
        }
        else if(!lastReached)
        {
            transform.Translate((wayPoints3D[wayPointNumber] - transform.position).normalized * payLoadSpeed * Time.deltaTime , Space.World);         
        }

        if (isRotating && !lastReached)
        {
            transform.forward = Vector3.Lerp(transform.forward, wayPoints3D[wayPointNumber] - wayPoints3D[wayPointNumber - 1], Time.deltaTime * 0.2f);
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(wayPoints3D[wayPoints3D.Length - 1].x, wayPoints3D[wayPoints3D.Length - 1].z)) < 1.0f)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }
}
