using UnityEngine;
using System.Collections;

public class Audio: MonoBehaviour {

	public AudioSource mouseClick;
	public AudioSource undo;
	public AudioSource wrongClick;
	public AudioSource backgroundMusic;


	// Use this for initialization
	void Start () {

		backgroundMusic.Play ();

	}
	
	// Update is called once per frame




	void Update () 
	{
		
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
