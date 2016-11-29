using UnityEngine;

public class wasdMovement : MonoBehaviour
{
    private const float MAX_VELOCITY = 50.0f;
    public float playerAcceleration;
    private Rigidbody playerRigidBody;
    public Camera mainCamera;
    PauseMenu pauseMenuScript;
    public bool countDownDone = false;
    public MouseLook mouseLook;
    public static CountdownTimerScript countdownTimer;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        mouseLook = new MouseLook();
        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            ResetVelocities();
        }
        playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * playerAcceleration * Time.deltaTime, 0, Input.GetAxis("Vertical") * playerAcceleration * Time.deltaTime, ForceMode.VelocityChange);
        playerRigidBody.velocity = Vector3.ClampMagnitude(playerRigidBody.velocity, MAX_VELOCITY);

        if (!pauseMenuScript.isPaused && countDownDone)
        {
            mouseLook.LookRotation(transform, mainCamera.transform);
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