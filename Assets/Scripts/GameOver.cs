using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	Image redScreenImageScript;
	Text gameOverTextScript;
	[HideInInspector]
	public bool isGameOver;
	[Range(1.0f,5.0f)]
	public float speedSplashScreen;

	void Start()
	{
		isGameOver = false;
		redScreenImageScript = transform.GetChild (0).GetComponent<Image>();
		gameOverTextScript = transform.GetChild (1).GetComponent<Text>();
	}

	void Update()
	{
		if (isGameOver) 
		{
			redScreenImageScript.color += new Color (0.0f, 0.0f, 0.0f, Time.deltaTime / speedSplashScreen);
			gameOverTextScript.color += new Color (0.0f, 0.0f, 0.0f, Time.deltaTime / speedSplashScreen);
		}
		if (redScreenImageScript.color.a >= 0.99f) 
		{
            GameManager.Instance.goToMenu();
		}
	}
}
