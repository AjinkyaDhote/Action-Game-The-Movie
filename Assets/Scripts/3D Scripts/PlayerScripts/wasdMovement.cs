using UnityEngine;

public class wasdMovement : MonoBehaviour
{
    private const float MAX_VELOCITY = 25.0f;
    public float playerAcceleration;
    private Rigidbody playerRigidBody;
    private float horizontal;
    private float vertical;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            playerRigidBody.AddRelativeForce(-horizontal * playerAcceleration * Time.deltaTime, 0, -vertical * playerAcceleration * Time.deltaTime, ForceMode.VelocityChange);
            ResetVelocities();
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            playerRigidBody.AddRelativeForce(horizontal * playerAcceleration * Time.deltaTime, 0, vertical * playerAcceleration * Time.deltaTime, ForceMode.VelocityChange);
            playerRigidBody.velocity = Vector3.ClampMagnitude(playerRigidBody.velocity, MAX_VELOCITY);
        }
    }

    void ResetVelocities()
    {
        if (playerRigidBody.velocity != Vector3.zero)
        {
            playerRigidBody.velocity = Vector3.zero;
        }
        if (playerRigidBody.angularVelocity != Vector3.zero)
        {
            playerRigidBody.angularVelocity = Vector3.zero;
        }
    }
}