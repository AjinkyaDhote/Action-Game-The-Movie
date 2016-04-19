using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimerScript : MonoBehaviour {

    Camera gunCamera;
    [HideInInspector]
    public bool hasGameStarted = false;
    //bool countdownStarted = false;
    //Text countdownText;
    //float countdown = 3;
    //float initialTime;
    //float elapsedTime = 0.0f;

	void Start () {

        gunCamera = transform.parent.GetComponent<Canvas>().worldCamera;
        gunCamera.cullingMask = gunCamera.cullingMask & 0x800;
        Time.timeScale = 0.0f;
        //countdownText = GetComponent<Text>();
    }
	

	void Update ()
    {
	// if(countdownStarted)
        //{
        //    countdown -= Time.deltaTime;
        //    countdownText.text = ((int)countdown).ToString();
        //    if(countdown <= 0)
        //    {
        //        gunCamera.cullingMask = gunCamera.cullingMask | 0x220;
        //        gameObject.SetActive(false);
        //        hasGameStarted = true;         
        //    }

        //}
	}

    public void ContinueClicked()
    {
        gunCamera.cullingMask = gunCamera.cullingMask | 0x220;
        transform.GetChild(0).gameObject.SetActive(false);
        // countdownStarted = true;
        // countdownText.text = ((int)countdown).ToString();
        hasGameStarted = true;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Destroy(gameObject);
    }
}
