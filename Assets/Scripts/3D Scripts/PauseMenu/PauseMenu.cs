using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    [HideInInspector]
    public bool isPaused;
    CountdownTimerScript countdownTimer;
    AudioSource backgroundMusic;

    Camera gunCamera;
    void Start()
    {
        isPaused = false;
        gunCamera = transform.GetComponent<Canvas>().worldCamera;
        gunCamera.cullingMask = gunCamera.cullingMask & 0xbff;
        GameObject instructionsCanvas = GameObject.FindWithTag("InstructionsCanvas");
        countdownTimer = instructionsCanvas.transform.GetChild(0).GetComponent<CountdownTimerScript>();
        backgroundMusic = instructionsCanvas.GetComponent<AudioSource>();
    }
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && countdownTimer.hasGameStarted)
        {
            isPaused = !isPaused;
            ChangePauseState();
        }
	}
    void ChangePauseState()
    {
        if(isPaused)
        {
            backgroundMusic.Pause();
            gunCamera.cullingMask = gunCamera.cullingMask | 0x400;
            gunCamera.cullingMask = gunCamera.cullingMask & 0xddf;
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            backgroundMusic.UnPause();
            gunCamera.cullingMask = gunCamera.cullingMask | 0x220;
            gunCamera.cullingMask = gunCamera.cullingMask & 0xbff;
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void GoBackToMainMenu()
    {
        if(isPaused)
        {
            Time.timeScale = 1.0f;
            GameManager.Instance.GoToMenu();
        }   
    }
}
