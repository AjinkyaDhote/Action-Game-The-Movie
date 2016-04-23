using UnityEngine;

public class wasdMovement : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-(Time.deltaTime) * 20, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, (Time.deltaTime) * 20);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -(Time.deltaTime) * 20);

        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate((Time.deltaTime) * 20, 0, 0);
        }
        
    }
}