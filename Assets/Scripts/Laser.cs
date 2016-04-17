using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    bool isCorrectEnemyDetected;
    bool isLaserDrawn;
    LineRenderer lineRenderer;
    Vector3 positionEnd;
    void Start()
    { 
        lineRenderer = GetComponent<LineRenderer>();
        isLaserDrawn = false;  
    }
	void Update ()
    {
        if (isCorrectEnemyDetected)
        {
            if (!isLaserDrawn)
            {
                lineRenderer.SetPosition(1, positionEnd);
                isLaserDrawn = true;
            }
            Destroy(gameObject, 0.1f);
        }
	}
    public void GetCorrectEnemy(Vector3 hitPoint)
    {
        isCorrectEnemyDetected = true;
        positionEnd = transform.InverseTransformPoint(hitPoint);
    }
}
