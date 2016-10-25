using UnityEngine;

public class wasdMovement : MonoBehaviour
{
    private const float MAX_VELOCITY = 25.0f;
    public float playerAcceleration;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            ResetVelocities();
        }
        rb.AddRelativeForce(Input.GetAxis("Horizontal") * playerAcceleration * Time.deltaTime, 0, Input.GetAxis("Vertical") * playerAcceleration * Time.deltaTime, ForceMode.VelocityChange);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MAX_VELOCITY);
    }

    void ResetVelocities()
    {
        if (rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
        if (rb.angularVelocity != Vector3.zero)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}