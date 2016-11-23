using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseSensitivitySlider : MonoBehaviour {

    wasdMovement wasdMovementScript;
    Slider mouseSensitivitySlider;
    Text sensitivityValueText;

    void Start ()
    {
        wasdMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<wasdMovement>();
        mouseSensitivitySlider = GetComponent<Slider>();
        sensitivityValueText = transform.GetChild(4).gameObject.GetComponent<Text>();
        wasdMovementScript.mouseLook.XSensitivity = wasdMovementScript.mouseLook.YSensitivity = mouseSensitivitySlider.value;
        sensitivityValueText.text = mouseSensitivitySlider.value.ToString("f2");
    }
	
	public void SetMouseSentivity ()
    {
        wasdMovementScript.mouseLook.XSensitivity = wasdMovementScript.mouseLook.YSensitivity = mouseSensitivitySlider.value;
        sensitivityValueText.text = mouseSensitivitySlider.value.ToString("f2");
    }
}
