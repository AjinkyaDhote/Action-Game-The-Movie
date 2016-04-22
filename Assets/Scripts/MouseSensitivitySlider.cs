using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseSensitivitySlider : MonoBehaviour {

    PlayerMovement playerMovementScript;
    Slider mouseSensitivitySlider;
    Text sensitivityValueText;

    void Start ()
    {
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        mouseSensitivitySlider = GetComponent<Slider>();
        sensitivityValueText = transform.GetChild(4).gameObject.GetComponent<Text>();
        playerMovementScript.mouseLook.XSensitivity = playerMovementScript.mouseLook.YSensitivity = mouseSensitivitySlider.value;
        sensitivityValueText.text = mouseSensitivitySlider.value.ToString("f2");
    }
	
	public void SetMouseSentivity ()
    {
        playerMovementScript.mouseLook.XSensitivity = playerMovementScript.mouseLook.YSensitivity = mouseSensitivitySlider.value;
        sensitivityValueText.text = mouseSensitivitySlider.value.ToString("f2");
    }
}
