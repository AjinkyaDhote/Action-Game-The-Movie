using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	Image backgroundImageScript;
	Text messageScript;
	[Range(1.0f,5.0f)]
	public float speedSplashScreen;

	void Start()
	{
		backgroundImageScript = transform.GetChild (0).GetComponent<Image>();
		messageScript = transform.GetChild (1).GetComponent<Text>();
        messageScript.text = GameManager.Instance.win_Lose_Message;
        if (GameManager.Instance.win_Lose)
        {
            backgroundImageScript.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            messageScript.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else
        {
            backgroundImageScript.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            messageScript.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
    }

	void Update()
	{
        backgroundImageScript.color += new Color (0.0f, 0.0f, 0.0f, Time.deltaTime / speedSplashScreen);
		messageScript.color += new Color (0.0f, 0.0f, 0.0f, Time.deltaTime / speedSplashScreen);		
		if (backgroundImageScript.color.a >= 0.99f) 
		{
            GameManager.Instance.GoToMenu();
		}
        if (backgroundImageScript.color.a >= 0.5f)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    public void GoBackToMainMenu()
    {
        GameManager.Instance.GoToMenu();
    }
}
