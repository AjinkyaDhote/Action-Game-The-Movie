using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSlow : MonoBehaviour
{
    public float currentTime = 0;
    public float slowDuration = 1.5f;
    private Image image;
    private Slider slider;
    public float sliderFillRate = 0.2f;
    public float flashSpeed = 0.4f;
    public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 0.1f);
    bool isPressed = false;

    // Use this for initialization
    void Start()
    {
        image = transform.GetChild(0).gameObject.GetComponent<Image>();
        slider = transform.GetChild(2).gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && slider.value >= 50)
        {
            isPressed = true;
        }
        if (isPressed)
        {
            SlowTime();
        }
        else if(slider.value<=slider.maxValue)
        {
            slider.value += sliderFillRate;
        }
    }

    void SlowTime()

    {
        
        if (isPressed)
        {
            
            if (Time.timeScale == 1.0F && slider.value >= 50)
            {
                Time.timeScale = 0.3F;
                image.color = flashColor;
            }
        
            Time.fixedDeltaTime = 0.02F * Time.timeScale;

        }
            


        if (Time.timeScale == 0.3f)
        {
            
            currentTime += Time.deltaTime;

            slider.value -= currentTime;
        }

        //  if (currentTime > slowDuration)
        if (slider.value == 0)
        {
            image.color = Color.clear;
            currentTime = 0.0f;
            Time.timeScale = 1.0f;
            isPressed = false;
        }
    }

}
