using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
	public float initialHealth;
    Slider healthSlider;
    Image barColorImage;

    void Start()
    {
        healthSlider = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("HealthSlider").GetComponent<Slider>();
        healthSlider.minValue = 0;
        healthSlider.maxValue = initialHealth;
        healthSlider.value = initialHealth;
        barColorImage = healthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        barColorImage.color = Color.green;
    }

    public void PlayerDamage(float damage)
    {
        healthSlider.value -= damage;

        if (healthSlider.value <= (initialHealth / 4))
        {
            barColorImage.color = Color.red;
        }
        else if (healthSlider.value <= (initialHealth / 2))
        {
            barColorImage.color = Color.yellow;
        }

        if (healthSlider.value <= 0)
        {
            barColorImage.color = Color.clear;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Instance.win_Lose = false;
            GameManager.Instance.win_Lose_Message = "Game Over";
            GameManager.Instance.GoToWinLoseScene();
        }
    }
}
