using UnityEngine;

[System.Serializable]
public class MyAudioSource
{
    [Tooltip("This helps to adjust the frequency with which the given sound is played." +
                "(Note: 0 and 1 has no effect. The sound will be played always)")]
    public uint frequencyModifier = 1;
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    public bool loop = false;
    public bool playOnAwake = false;
    [HideInInspector]
    public AudioSource audioSource;
    private uint playCount = 0;

    public static void Initilaize(GameObject gameObject, MyAudioSource myAudioSource)
    {
        myAudioSource.audioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.audioSource.clip = myAudioSource.audioClip;
        myAudioSource.audioSource.volume = myAudioSource.volume;
        myAudioSource.audioSource.loop = myAudioSource.loop;
        myAudioSource.audioSource.playOnAwake = myAudioSource.playOnAwake;
        myAudioSource.playCount = 0;
    }

    public void Play()
    {
        if (frequencyModifier != 0)
        {
            if (playCount % frequencyModifier == 0)
            {
                audioSource.Play();
                playCount++;
            }
        }
        else
        {
            audioSource.Play();
        }
    }
}
