using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RMBHoldToggle : MonoBehaviour {
    TimeSlow timeSlowScript;
    Text labelText;
    string[] messages;
    void Start()
    {
        messages = new string[] { "HOLD RMB for BULLET TIME", "TOGGLE RMB for BULLET TIME" };
        timeSlowScript = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas").FindChild("BulletTime").GetComponent<TimeSlow>();
        timeSlowScript.isRMBHoldType = true;
        labelText = transform.GetChild(0).GetComponent<Text>();
        labelText.text = messages[0];
    }
    public void ChangeRMBImplementation()
    {
        timeSlowScript.isRMBHoldType = !timeSlowScript.isRMBHoldType;
        labelText.text = messages[timeSlowScript.isRMBHoldType ? 0 : 1];
    }
}
