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

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-(Time.deltaTime) * _playerSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, (Time.deltaTime) * _playerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -(Time.deltaTime) * _playerSpeed);

        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate((Time.deltaTime) * _playerSpeed, 0, 0);
        }
        
    }
}