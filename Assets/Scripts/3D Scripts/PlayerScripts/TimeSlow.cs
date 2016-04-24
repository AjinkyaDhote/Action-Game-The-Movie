using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSlow : MonoBehaviour
{
    public float sliderFillRate = 0.2f;
    public float sliderDepleteRate = 0.5f;
    [Range(0.0f,1.0f)]
    public float reducedTimeScale = 0.3f;
    [Range(0.0f, 100.0f)]
    public float sliderValueActive = 50.0f;
    public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 0.1f);
    public float coolDownTimer = 5.0f;
    [HideInInspector]
    public bool isRMBHoldType;

    Image tintImageScript;
    Image clockImageScript;
    Image sliderFillImageScript;
    Slider slider;
    CountdownTimerScript countdownTimer;
    PauseMenu pauseMenuScript;
    float initialFixedDeltaTime;
    bool isPressed;
    bool isRefilling;
    bool isSlowTimeDisabled;
    bool isSlowTimeEnabledInToggleMode;
    void Start()
    {
        isPressed = false;
        isRefilling = false;
        isSlowTimeDisabled = false;
        isSlowTimeEnabledInToggleMode = true;
        initialFixedDeltaTime = Time.fixedDeltaTime;
        tintImageScript = transform.GetChild(0).GetComponent<Image>();
        clockImageScript=transform.GetChild(1).GetComponent<Image>();
        sliderFillImageScript = transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Image>();
        slider = transform.GetChild(2).GetComponent<Slider>();
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();
    }

    void Update()
    {
        if(pauseMenuScript.isPaused)
        {
            if (isPressed)
            {
                ResetBulletTime();
            }
        }
        if(countdownTimer.hasGameStarted && !pauseMenuScript.isPaused)
        {
            if (isRMBHoldType)
            {
                if (Input.GetButton("Fire2") && !isRefilling && !isSlowTimeDisabled)
                {
                    isPressed = true;
                }
                else if (Input.GetButtonUp("Fire2"))
                {
                    ResetBulletTime();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire2") && !isRefilling && !isSlowTimeDisabled)
                {
                    if (isSlowTimeEnabledInToggleMode)
                    {
                        isPressed = true;
                        isSlowTimeEnabledInToggleMode = false;
                    }
                    else
                    {     
                        ResetBulletTime();
                    }
                }
            }           
        }   
        if (isPressed)
        {
            SlowTime();
        }
        else if (slider.value <= slider.maxValue)
        {
            slider.value += sliderFillRate;
            if(slider.value >= sliderValueActive)
            {
                isRefilling = false;
            }
        }
        if (isSlowTimeDisabled || slider.value <= sliderValueActive)
        {
            clockImageScript.color = sliderFillImageScript.color = Color.red;
        }
        else
        {
            clockImageScript.color = sliderFillImageScript.color = Color.white;
        }
    }
    void SlowTime()
    {
        if (isPressed && (!isSlowTimeDisabled))
        {
            if (Time.timeScale == 1.0f && slider.value >= sliderValueActive)
            {
                Time.timeScale = reducedTimeScale;
                tintImageScript.color = flashColor;
                Time.fixedDeltaTime = initialFixedDeltaTime * Time.timeScale;
            }
            if (Time.timeScale == reducedTimeScale)
            {
                slider.value -= sliderDepleteRate;
            }
            if (slider.value == slider.minValue)
            {
                if(!isSlowTimeEnabledInToggleMode)
                {
                    ResetBulletTime();
                }
                else
                {
                    tintImageScript.color = Color.clear;
                    Time.timeScale = 1.0f;
                    isPressed = false;
                    Time.fixedDeltaTime = initialFixedDeltaTime * Time.timeScale;
                }            
            }
        }       
    }
    void ResetBulletTime()
    {
        tintImageScript.color = Color.clear;
        if(pauseMenuScript.isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = initialFixedDeltaTime * Time.timeScale;
        }   
        isPressed = false;     
        isSlowTimeEnabledInToggleMode = true;
        if (!isSlowTimeDisabled)
        {
            isSlowTimeDisabled = true;
            StartCoroutine("CoolDownWait");
        }
        if (slider.value < sliderValueActive)
        {
            isRefilling = true;
        }
    }
    IEnumerator CoolDownWait()
    {
        yield return new WaitForSeconds(coolDownTimer);
        isSlowTimeDisabled = false;
    }
}
