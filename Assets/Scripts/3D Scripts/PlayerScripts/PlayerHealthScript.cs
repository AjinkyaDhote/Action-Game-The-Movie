using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    Text HealthText;
	public float healthCount;
    string healthString;

    void Start()
    {
        HealthText = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("HealthText").GetComponent<Text>();
        healthString = " " + healthCount;
		HealthText.color = Color.white;
        HealthText.text = healthString;
    }

    public void PlayerDamage(float damage)
    {
        healthCount -= (damage);
        healthString = "" + healthCount.ToString("0");
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
