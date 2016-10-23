﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
	public float initialHealth;
    Slider healthSlider;
    Image barColorImage;
    Material glitchMaterial;
    bool isGlitchEffectResetNeeded = true;

    void Start()
    {       
        healthSlider = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas").FindChild("HealthSlider").GetComponent<Slider>();
        healthSlider.minValue = 0;
        healthSlider.maxValue = initialHealth;
        healthSlider.value = initialHealth;
        barColorImage = healthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        barColorImage.color = Color.green;
        glitchMaterial = Camera.main.GetComponent<ScreenGlitch>().glitchMaterial;
    }

    public void PlayerRegenerate(float health)
    {
        if(isGlitchEffectResetNeeded)
        {
            glitchMaterial.SetFloat("_Magnitude", 0.0f);
        }     
        healthSlider.value += health;

        if (healthSlider.value >= (initialHealth / 2))
        {
            barColorImage.color = Color.green;
        }
        else if (healthSlider.value >= (initialHealth / 4))
        {
            barColorImage.color = Color.yellow;
        }
    }

    public void PlayerDamage(float damage, float glitchIntensity, string id = null)
    {
        if (id != null)
        {
            isGlitchEffectResetNeeded = false;
            StartCoroutine(SetIsGlitchEffectResetNeeded());
        }
        glitchMaterial.SetFloat("_Magnitude", glitchIntensity);
        //StartCoroutine(SetGlitch());
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
    IEnumerator SetIsGlitchEffectResetNeeded()
    {
        yield return new WaitForSeconds(1.5f);
        isGlitchEffectResetNeeded = true;
    }
}
