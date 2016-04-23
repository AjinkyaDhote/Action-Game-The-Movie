using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour {
    [Range(0.0f,8.0f)]
    public float minIntensity = 0.0f;
    [Range(0.0f, 8.0f)]
    public float maxIntensity = 8.0f;
    public int minOnOffValue = 1;
    public int maxOnOffValue = 6;

    Light flickerLight;
    bool isLightOn;
    bool isCoroutineStarted;

    void Start()
    {
        flickerLight = GetComponent<Light>();
        flickerLight.intensity = maxIntensity;
        isLightOn = true;
        isCoroutineStarted = false;
    }
     void Update()
    {
        if(!isCoroutineStarted)
        {
            if (isLightOn)
            {
                flickerLight.intensity = minIntensity;
                StartCoroutine("ChangeOnOffStatusOfLight");
                isCoroutineStarted = true;
            }
            else
            {
                flickerLight.intensity = maxIntensity;
                StartCoroutine("ChangeOnOffStatusOfLight");
                isCoroutineStarted = true;
            }
        }
    }
    IEnumerator ChangeOnOffStatusOfLight()
    {
        yield return new WaitForSeconds(Random.Range(minOnOffValue, maxOnOffValue));
        isLightOn = !isLightOn;
        isCoroutineStarted = false;
    }
}
