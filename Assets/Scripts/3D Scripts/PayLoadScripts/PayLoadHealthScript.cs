using UnityEngine;
using System.Collections;
using System.Text;

public class PayLoadHealthScript : MonoBehaviour
{
    const int NUMBER_OF_PARTS_FOR_HEALTH = 5;
    const float STOP_PAYLOAD_MOVEMENT_TIME = 5.0f;

    public int payLoadHealth;

    int initialPayLoadHealth;

    TextMesh payLoadHealthText;
    StringBuilder payLoadHealthString;
    int numberOfLs = 0;

    int resetPayloadSpeedValue;
    PayLoadMovementScript payLoadMovementScript;
    float resetPayloadSpeedTime;

    Transform playerTransform;

    AudioSource hitAudioSource;
    void Start()
    {
        hitAudioSource = GetComponent<AudioSource>();
        payLoadMovementScript = transform.parent.gameObject.GetComponent<PayLoadMovementScript>();
        resetPayloadSpeedValue = payLoadMovementScript.payLoadSpeed;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        payLoadHealthString = new StringBuilder();
        int remainder = payLoadHealth % NUMBER_OF_PARTS_FOR_HEALTH;
        if (remainder == 0)
        {
            numberOfLs = payLoadHealth / NUMBER_OF_PARTS_FOR_HEALTH;
        }
        else
        {
            payLoadHealth -= remainder;
            numberOfLs = payLoadHealth / NUMBER_OF_PARTS_FOR_HEALTH;
        }
        initialPayLoadHealth = payLoadHealth;
        payLoadHealthText = GetComponent<TextMesh>();
        payLoadHealthString = payLoadHealthString.Append('l', numberOfLs);
        payLoadHealthText.text = payLoadHealthString.ToString();
        payLoadHealthText.color = Color.green;
    }

    private void Update()
    {
        transform.LookAt(playerTransform);
        if (Time.realtimeSinceStartup > resetPayloadSpeedTime)
        {
            payLoadMovementScript.payLoadSpeed = resetPayloadSpeedValue;
        }
    }

    public void PayLoadDamage()
    {
        hitAudioSource.Play();
        resetPayloadSpeedTime = Time.realtimeSinceStartup + STOP_PAYLOAD_MOVEMENT_TIME;
        payLoadMovementScript.payLoadSpeed = 0;
        payLoadHealth -= NUMBER_OF_PARTS_FOR_HEALTH;
        numberOfLs--;
        payLoadHealthString.Length = 0;
        payLoadHealthString = payLoadHealthString.Append('l', numberOfLs);
        payLoadHealthText.text = payLoadHealthString.ToString();
        if (payLoadHealth <= (initialPayLoadHealth / 4))
        {
            payLoadHealthText.color = Color.red;
        }
        else if (payLoadHealth <= (initialPayLoadHealth / 2))
        {
            payLoadHealthText.color = Color.yellow;
        }

        if (payLoadHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Instance.win_Lose = false;
            GameManager.Instance.win_Lose_Message = "Game Over";
            GameManager.Instance.GoToWinLoseScene();
        }
    }
}
