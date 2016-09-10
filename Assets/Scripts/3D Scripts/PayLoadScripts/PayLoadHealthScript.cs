using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PayLoadHealthScript : MonoBehaviour {

    Text payLoadHealthText;
    public float payLoadHealth;
    string payLoadHealthString;

	void Start ()
    {
        payLoadHealthText = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("PayLoadHealthText").GetComponent<Text>();
        payLoadHealthString = " " + payLoadHealth;
        payLoadHealthText.color = Color.white;
        payLoadHealthText.text = payLoadHealthString;
    }

    public void PayLoadDamage(float damage)
    {
        payLoadHealth -= damage;
        payLoadHealthString = "" + payLoadHealth;
        payLoadHealthText.text = payLoadHealthString;

        if(payLoadHealth <= 30)
        {
            payLoadHealthText.color = Color.red;
        }

        if(payLoadHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Instance.win_Lose = false;
            GameManager.Instance.win_Lose_Message = "Game Over";
            GameManager.Instance.GoToWinLoseScene();
        }
    } 	    	
}
