using UnityEngine;
using System.Collections;

public class PlayerInRange : MonoBehaviour
{
    AI_movement aiMovementScript;
    void Start()
    {
        aiMovementScript = transform.parent.GetComponent<AI_movement>();
    }
    void OnTriggerEnter(Collider other)
    {
        aiMovementScript.InRange();
    }
    void OnTriggerExit()
    {
        aiMovementScript.OutOfRange();
    }
}
