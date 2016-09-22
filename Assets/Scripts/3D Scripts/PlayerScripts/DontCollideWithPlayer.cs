using UnityEngine;
using System.Collections;

public class DontCollideWithPlayer : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerRigidbody.velocity = playerRigidbody.angularVelocity = Vector3.zero;
        playerRigidbody.position = transform.position;
    }
}
