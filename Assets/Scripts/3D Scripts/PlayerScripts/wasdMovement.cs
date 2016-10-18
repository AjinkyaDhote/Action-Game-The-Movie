using UnityEngine;

public class wasdMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 100000.0f;
    public float PlayerSpeed
    {
        get
        {
            return _playerSpeed;
        }
        set
        {
            _playerSpeed = value;
        }
    }

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position + (-transform.right) * Time.deltaTime  * _playerSpeed);
            //rb.MovePosition(new Vector3(-(Time.deltaTime) * _playerSpeed, 0f, 0f));
            //transform.Translate(-(Time.deltaTime) * _playerSpeed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * _playerSpeed);
            //rb.MovePosition(new Vector3(0f, 0f, (Time.deltaTime) * _playerSpeed));
            //transform.Translate(0, 0, (Time.deltaTime) * _playerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position + (-transform.forward) * Time.deltaTime * _playerSpeed);
            //rb.MovePosition(new Vector3(0f, 0f, -(Time.deltaTime) * _playerSpeed));
            //transform.Translate(0, 0, -(Time.deltaTime) * _playerSpeed);

        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * _playerSpeed);
            //rb.MovePosition(new Vector3((Time.deltaTime) * _playerSpeed, 0f, 0f));
            //transform.Translate((Time.deltaTime) * _playerSpeed, 0, 0);
        }
        
    }
}