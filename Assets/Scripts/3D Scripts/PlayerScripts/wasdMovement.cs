using UnityEngine;

public class wasdMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 10.0f;
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
            if (rb.velocity != Vector3.zero && rb.angularVelocity != Vector3.zero)
            {
                rb.velocity = rb.angularVelocity = Vector3.zero;
            }
            //rb.MovePosition(transform.position + (-transform.right) * Time.deltaTime  * _playerSpeed);
            //rb.MovePosition(new Vector3(-(Time.deltaTime) * _playerSpeed, 0f, 0f));     
            transform.Translate(-(Time.deltaTime) * _playerSpeed,0f,0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (rb.velocity != Vector3.zero && rb.angularVelocity != Vector3.zero)
            {
                rb.velocity = rb.angularVelocity = Vector3.zero;
            }
            //rb.MovePosition(transform.position + transform.forward * Time.deltaTime * _playerSpeed);
            //rb.MovePosition(new Vector3(0f, 0f, (Time.deltaTime) * _playerSpeed));
            rb.velocity = rb.angularVelocity = Vector3.zero;
            transform.Translate(0, 0, (Time.deltaTime) * _playerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (rb.velocity != Vector3.zero && rb.angularVelocity != Vector3.zero)
            {
                rb.velocity = rb.angularVelocity = Vector3.zero;
            }
            //rb.MovePosition(transform.position + (-transform.forward) * Time.deltaTime * _playerSpeed);
            //rb.MovePosition(new Vector3(0f, 0f, -(Time.deltaTime) * _playerSpeed));
            rb.velocity = rb.angularVelocity = Vector3.zero;
            transform.Translate(0, 0, -(Time.deltaTime) * _playerSpeed);

        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rb.velocity != Vector3.zero && rb.angularVelocity != Vector3.zero)
            {
                rb.velocity = rb.angularVelocity = Vector3.zero;
            }
            //rb.MovePosition(transform.position + transform.right * Time.deltaTime * _playerSpeed);
            //rb.MovePosition(new Vector3((Time.deltaTime) * _playerSpeed, 0f, 0f));
            rb.velocity = rb.angularVelocity = Vector3.zero;
            transform.Translate((Time.deltaTime) * _playerSpeed, 0, 0);
        }
        
    }
}