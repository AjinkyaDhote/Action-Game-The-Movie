using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    Text HealthText;
    private int healthCount = 10;
    string healthString;

    void Start()
    {
        HealthText = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("HealthText").GetComponent<Text>();
        healthString = " " + healthCount;
        //isPlayerDead = false;
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
            GameManager.Instance.win_Lose = false;
            GameManager.Instance.win_Lose_Message = "Game Over";
            GameManager.Instance.GoToWinLoseScene();
        }
    }
}
