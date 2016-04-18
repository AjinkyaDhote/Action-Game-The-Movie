using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    [HideInInspector]
    public bool isPaused;

    Camera gunCamera;
    void Start()
    {
        isPaused = false;
        gunCamera = transform.GetComponent<Canvas>().worldCamera;
        gunCamera.cullingMask = gunCamera.cullingMask & 0xbff;
    }
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            ChangePauseState();
        }
	}
    void ChangePauseState()
    {
        if(isPaused)
        {
            gunCamera.cullingMask = gunCamera.cullingMask | 0x400;
            gunCamera.cullingMask = gunCamera.cullingMask & 0xfdf;
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            gunCamera.cullingMask = gunCamera.cullingMask | 0x20;
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
