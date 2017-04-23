using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VideoController : MonoBehaviour
{
    private MovieTexture _logo;
    private MovieTexture _cutscene;

    private Renderer _renderer;
    private AudioSource _audioSource;

    private bool _loadGame;
    private GameObject _skipText;

    private void Start()
    {
        _logo = Resources.Load<MovieTexture>("MovieTextures/Logo");
        _cutscene = Resources.Load<MovieTexture>("MovieTextures/Cutscene");
        _renderer = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
        _skipText = GameObject.Find("SkipText");
        _skipText.SetActive(false);
        
        _renderer.material.mainTexture = _logo;
        _audioSource.clip = _logo.audioClip;

        _logo.Play();
        _audioSource.Play();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(TimeToWaitToShowSkipMessage());
    }

    private void Update()
    {
        if (_logo.isPlaying)return;
        if (Input.GetKeyDown(KeyCode.Space)) GameManager.Instance.GoToMenu();
        if (_cutscene.isPlaying) return;
        if(!_cutscene.isPlaying && _loadGame) GameManager.Instance.GoToMenu();
        _renderer.material.mainTexture = _cutscene;
        _audioSource.clip = _cutscene.audioClip;

        _cutscene.Play();
        _audioSource.Play();
        _loadGame = true;
    }

    IEnumerator TimeToWaitToShowSkipMessage()
    {
        yield return new WaitForSeconds(6.0f);
        _skipText.SetActive(true);
    }
}
