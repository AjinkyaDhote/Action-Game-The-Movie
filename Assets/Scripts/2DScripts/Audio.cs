using UnityEngine;
using System.Collections;

public class Audio: MonoBehaviour {

	public AudioSource mouseClick;
	public AudioSource undo;
	public AudioSource wrongClick;
    public AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic.Play();
    }

	public void MouseClicked()
	{
		mouseClick.Play ();		
	}

	public void WrongClick()
	{
		wrongClick.Play ();
	}

	public void Undo()
	{
		undo.Play ();
	}
}
