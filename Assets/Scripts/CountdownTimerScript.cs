using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimerScript : MonoBehaviour {

    Camera gunCamera;
    [HideInInspector]
    public bool hasGameStarted = false;
    bool countdownStarted;
    Text countdownText;
    int countdown;
    float initialTime;
    AudioSource backgroundMusic;

    void Start ()
    {
        backgroundMusic = GetComponentInParent<AudioSource>();
        countdownStarted = false;
        countdown = 3;
        gunCamera = transform.parent.GetComponent<Canvas>().worldCamera;
        gunCamera.cullingMask = gunCamera.cullingMask & 0x800;
        Time.timeScale = 0.0f;
        countdownText = GetComponent<Text>();
    }
	void Update ()
    {
        if (countdownStarted)
        {
            if (Time.realtimeSinceStartup - initialTime >= 1.0f)
            {
                countdown--;
                countdownText.text = countdown.ToString();
                initialTime = Time.realtimeSinceStartup;
            }            
            if (countdown == 0)
            {
                gunCamera.cullingMask = gunCamera.cullingMask | 0x220;
                countdownText.enabled = false;
                hasGameStarted = true;
                Time.timeScale = 1.0f;
                countdownStarted = false;
                GameObject.Find("FPSPlayer").GetComponent<wasdMovement>().countDownDone = true;
                backgroundMusic.Play();
            }
        }
    }

    public void ContinueClicked()
    {
        transform.GetChild(0).gameObject.SetActive(false);       
        countdownText.text = countdown.ToString();       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialTime = Time.realtimeSinceStartup;
        countdownStarted = true;
    }
}
