using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    Text HealthText;
    private int healthCount = 100;
    string healthString;

    //bool isPlayerDead;
    GameObject GameOverCamera;
    GameOver gameOverScript;

    // Use this for initialization
    void Start()
    {
        HealthText = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("HealthText").GetComponent<Text>();
        healthString = " " + healthCount;
        //isPlayerDead = false;
        GameOverCamera = GameObject.Find("GameOverCamera");
        gameOverScript = GameObject.Find("GameOverCanvas").GetComponent<GameOver>();
    }

    public void PlayerDamage()
    {
        healthCount -= 10;
        healthString = "" + healthCount;
        HealthText.text = healthString;

        if (healthCount <= 30)
        {
            HealthText.color = Color.red;
        }

        if (healthCount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameOverCamera.GetComponent<Camera>().enabled = true;
            GameOverCamera.GetComponent<AudioListener>().enabled = true;
            gameOverScript.isGameOver = true;
            //isPlayerDead = true;
            gameObject.SetActive(false);
        }

    }
}
