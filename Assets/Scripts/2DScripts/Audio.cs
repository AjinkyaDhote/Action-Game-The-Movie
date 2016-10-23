using UnityEngine;
using System.Collections;

public class Audio: MonoBehaviour {

	public AudioSource mouseClick;
	public AudioSource undo;
	public AudioSource wrongClick;
    public AudioSource backgroundMusic;
    public AudioSource batteryPickup;
    public AudioSource ammoPickup;
    public AudioSource targetReached;

    public AudioSource pickupHover;
    public AudioSource targetHover;


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

    public void BatteryPickup()
    {
        batteryPickup.Play();
    }

    public void AmmoPickup()
    {
        ammoPickup.Play();
    }

    public void TargetReached()
    {
        targetReached.Play();
    }

    public void PickupHover()
    {
        pickupHover.Play();
    }

    public void TargetHover()
    {
        targetHover.Play();
    }
}
