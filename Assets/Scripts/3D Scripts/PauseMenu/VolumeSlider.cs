using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private Text volumeValueText;
    private int length;

    // Use this for initialization
    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeValueText = transform.GetChild(4).gameObject.GetComponent<Text>();
        volumeValueText.text = volumeSlider.value.ToString("f2");
        UpdateVolumeOnAllSources();
        volumeSlider.onValueChanged.AddListener(OnSliderVolumeValueChanged);
        length = SoundManager3D.Instance.myAudioSources.Length;
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnSliderVolumeValueChanged);
    }

    private void OnSliderVolumeValueChanged(float value)
    {
        volumeValueText.text = value.ToString("f2");
        UpdateVolumeOnAllSources();
    }
    private void UpdateVolumeOnAllSources()
    {
        for (int i = 0; i < length; i++)
        {
            SoundManager3D.Instance.myAudioSources[i].audioSource.volume = volumeSlider.value;
        }
    }
}
