﻿using UnityEngine;
using System.Collections;

public class PathRenderer : MonoBehaviour {

    public GameObject prefab;
    //GameObject objOne;
    //public GameObject objTwo;
    private Vector3 posOne;
    private Vector3 posTwo;
    private GameObject go;
    float angle;
    public GameObject nodePrefab;
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
            GameObject node = (GameObject)Instantiate(nodePrefab, new Vector3 (posTwo.x,0.11f,posTwo.z), Quaternion.Euler(0, angle, 0));
            node.transform.localScale = new Vector3(1.5f, 0, 1.5f);
            //Debug.Log("1: " + wayPoints3D[0]);
            //Debug.Log("2: " + posTwo);

            go = (GameObject)Instantiate(prefab, new Vector3(xValue, 0, zValue), Quaternion.Euler(0, angle, 0));
            xValue = (posOne.x + posTwo.x) / 2;
            zValue = (posOne.z + posTwo.z) / 2;
            distance = Vector3.Distance(posOne, posTwo);
            //Debug.Log("" + distance);
            go.transform.localScale = new Vector3(distance / 10, 1, 0.1f);
            //Debug.Log("" + go.transform.localPosition);
            go.transform.localPosition = new Vector3(xValue, 0.1f, zValue);
            float debugAngle = Mathf.Atan2((posTwo.z - posOne.z), (posTwo.x - posOne.x)) * Mathf.Rad2Deg;
            go.transform.localEulerAngles = new Vector3(0, -debugAngle, 0);
        }

    }
}
