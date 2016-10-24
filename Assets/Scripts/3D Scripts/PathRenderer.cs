using UnityEngine;
using System.Collections;

public class PathRenderer : MonoBehaviour {

    private GameObject planePrefab;
    //GameObject objOne;
    //public GameObject objTwo;
    private Vector3 posOne;
    private Vector3 posTwo;
    private GameObject go;
    float angle;
    private GameObject nodePrefab;
    Vector3[] wayPoints3D;

    float distance, xValue, zValue;
    private float width2DPlane, width3DPlane, height2DPlane, height3DPlane;
    Vector3 convertPoint(Vector2 relativePoint)
    {
        Vector3 returnVal;
        returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
        returnVal.y = 0.5f;
        returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
        return returnVal;
    }

    void Start()
    {
        planePrefab = Resources.Load("LinePrefab/Line") as GameObject;
        nodePrefab = Resources.Load("NodePrefab/Node") as GameObject;
        angle = 0;
        width2DPlane = GameManager.Instance.width2DPlane;
        height2DPlane = GameManager.Instance.height2DPlane;
        width3DPlane = GameManager.Instance.width3DPlane;
        height3DPlane = GameManager.Instance.height3DPlane;

        wayPoints3D = new Vector3[GameManager.Instance.mapPoints.Count];
        for (int i = 0; i < wayPoints3D.Length; i++)
        {
            wayPoints3D[i] = convertPoint(GameManager.Instance.mapPoints[i]);
        }

        for (int i = 0; i < wayPoints3D.Length-1; i++)
        {
            posOne = wayPoints3D[i];
            posTwo = wayPoints3D[i+1];

            GameObject node = (GameObject)Instantiate(nodePrefab, new Vector3 (posTwo.x,0.3f,posTwo.z), Quaternion.Euler(0, angle, 0));
            node.transform.localScale = new Vector3(1.5f, 0, 1.5f);
            //Debug.Log("1: " + wayPoints3D[0]);
            //Debug.Log("2: " + posTwo);
            
            //Debug.Log("" + Vector3.Distance(posOne, posTwo));
            int pathMultiplier = (int)Vector3.Distance(posOne, posTwo) / 8;
            //Debug.Log("" + pathMultiplier);
            for (int iterator = 1; iterator < pathMultiplier*2; iterator+=2)
            {
                go = (GameObject)Instantiate(planePrefab, new Vector3(xValue, 0, zValue), Quaternion.Euler(0, angle, 0));
                Vector3 differenceVector = posTwo - posOne;
                differenceVector /= 2*pathMultiplier;
                differenceVector *= iterator;
                xValue = (posOne.x + differenceVector.x);
                zValue = (posOne.z + differenceVector.z);
                distance = Vector3.Distance(posOne, posTwo);
                //Debug.Log("" + pathMultiplier);
                go.transform.localScale = new Vector3(distance /( 10* pathMultiplier), 1, 0.1f);
                //Debug.Log("" + go.transform.localPosition);
                go.transform.localPosition = new Vector3(xValue, 0.1f, zValue);
                float debugAngle = Mathf.Atan2((posTwo.z - posOne.z), (posTwo.x - posOne.x)) * Mathf.Rad2Deg;
                go.transform.localEulerAngles = new Vector3(0, -debugAngle, 0);
            }
            int shortPath = (int)Vector3.Distance(posOne, posTwo) ;
            if (pathMultiplier  == 0) {
                pathMultiplier = 1;
               // Debug.Log("MAke this path ");
            go = (GameObject)Instantiate(planePrefab, new Vector3(xValue, 0, zValue), Quaternion.Euler(0, angle, 0));
            Vector3 differenceVector = posTwo - posOne;
            differenceVector /= 2 * pathMultiplier;
            //differenceVector *= 1;
            xValue = (posOne.x + differenceVector.x);
            zValue = (posOne.z + differenceVector.z);
            distance = Vector3.Distance(posOne, posTwo);
            Debug.Log("" + pathMultiplier);
            go.transform.localScale = new Vector3((distance / (10 * pathMultiplier)), 1, 0.1f);
            Debug.Log("" + go.transform.localPosition);
            go.transform.localPosition = new Vector3(xValue, 0.1f, zValue);
            float debugAngle = Mathf.Atan2((posTwo.z - posOne.z), (posTwo.x - posOne.x)) * Mathf.Rad2Deg;
            go.transform.localEulerAngles = new Vector3(0, -debugAngle, 0);

          }
        }

    }
}
