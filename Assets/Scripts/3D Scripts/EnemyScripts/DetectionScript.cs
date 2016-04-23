using UnityEngine;
using System.Collections;

public class DetectionScript : MonoBehaviour {
    AI_movement aiMovementScript;
    void Start()
    {
        aiMovementScript = transform.parent.GetComponent<AI_movement>();
    }
    void OnTriggerEnter(Collider other)
	{
        aiMovementScript.Detection ();
	}
}
