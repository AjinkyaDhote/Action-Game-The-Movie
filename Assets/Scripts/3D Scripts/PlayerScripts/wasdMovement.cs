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
    [HideInInspector]
    public bool isMoving;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
		pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        mouseLook = new MouseLook();
        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();        isMoving = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            ResetVelocities();
            isMoving = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            isMoving = true;
        }

        if (!pauseMenuScript.isPaused && countDownDone && !GameManager.Instance.infoDialogue)
        {
            mouseLook.LookRotation(transform, mainCamera.transform);
        }
    }

    void FixedUpdate()
    {        
        playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * playerAcceleration * Time.deltaTime, 0, Input.GetAxis("Vertical") * playerAcceleration * Time.deltaTime, ForceMode.VelocityChange);
        playerRigidBody.velocity = Vector3.ClampMagnitude(playerRigidBody.velocity, MAX_VELOCITY);               
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