﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Animator anim;
    public ParticleSystem[] impacts;

    GameObject laserPrefab;
    GameObject laser;
    float nextFire = 0.0f;
    WeaponSystem weaponSystemScript;
    PauseMenu pauseMenuScript;
    CountdownTimerScript countdownTimer;
    private EnemyHealth damageScript;
    bool shooting = false;
    int bulletCount = 300;

    Text AmmoText;
    string bulletsString;

    void Start()
	{
		weaponSystemScript = GetComponent<WeaponSystem> ();
		AmmoText = transform.FindChild ("FPS UI Canvas").FindChild ("AmmoText").GetComponent<Text> ();
		bulletsString = " " + bulletCount;
		AmmoText.text = bulletsString;
		AmmoText.color = Color.green;
		pauseMenuScript = GameObject.FindWithTag ("PauseMenu").GetComponent<PauseMenu> ();
		laserPrefab = Resources.Load ("Laser Prefab/Laser") as GameObject;
		countdownTimer = GameObject.Find ("InstructionsCanvas").transform.GetChild (0).GetComponent<CountdownTimerScript> ();
		Component[] animators;

		animators = GetComponentsInChildren<Animator> ();
		foreach (Animator i in animators) {
			if (i.gameObject.name == "ShotGun") {
				anim = i;
				break;
			}
		}
	}
    void Update()
    {
        if(weaponSystemScript.currentWeaponInHand.Value.name == "MachineGun")
        {
            if (Input.GetButton("Fire1") && !pauseMenuScript.isPaused && countdownTimer.hasGameStarted)
            {  
                shooting = true;
            }
        }
        else if (Input.GetButtonDown("Fire1") && !pauseMenuScript.isPaused && Time.time > nextFire && countdownTimer.hasGameStarted)
        {
            nextFire = Time.time + weaponSystemScript.currentWeaponInfo.coolDownTimer;
            shooting = true;
        }
    }


    void FixedUpdate()
	{     

        if (shooting && bulletCount > 0)
        {
			muzzleFlash.Play();
			if (weaponSystemScript.currentWeaponInHand.Value.name == "ShotGun") 
			{
				Debug.Log ("First ShotGun Animation");
				anim.SetTrigger("ShotGun");
			}
			else if(weaponSystemScript.currentWeaponInHand.Value.name == "GravityGun" || weaponSystemScript.currentWeaponInHand.Value.name == "MachineGun" )
			{
				anim.SetTrigger("Fire");
			}

			if (bulletCount > 0) 
			{
				bulletCount -= weaponSystemScript.currentWeaponInfo.ammoNeeded;
				bulletsString = " " + bulletCount;
				AmmoText.text = bulletsString;
			}

			if (bulletCount <= 10)
			{
				AmmoText.color = Color.red;
			}

				
            shooting = false;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            {
                if (weaponSystemScript.currentWeaponInHand.Value.name == "GravityGun")
                {
                    laser = Instantiate(laserPrefab);
                    laser.transform.SetParent(transform);
                    laser.transform.localPosition = new Vector3(0.695f, -0.809f, 2.805f);
                    laser.transform.localRotation = Quaternion.identity;
                    laser.GetComponent<Laser>().GetCorrectEnemy(hit.point);
                }
                print(hit.collider.tag);
                if (hit.collider.tag == "Wall")
                {
                    impacts[0].transform.position = hit.point;
                    Debug.Log(hit.point);
                    impacts[0].Play();
                }
                else if (hit.collider.tag == "BodyCollider")
                {

                    impacts[1].transform.position = hit.point;
                    impacts[1].Play();
                    AI_movement aiMovementScript = hit.collider.transform.parent.parent.GetComponent<AI_movement>();
                    if (aiMovementScript != null)
                    {
                        if (!aiMovementScript.isPlayerSeen)
                        {
                            aiMovementScript.Detection();
                        }
                    }
                     damageScript = hit.collider.GetComponent<EnemyHealth>();
                    if (damageScript != null)
                    {
                        Debug.Log("DamageDealt");
                        damageScript.Damage(weaponSystemScript.currentWeaponInfo.damageDealt);
                    }
                }

                else if ((hit.collider.tag == "HeadCollider"))
                {
                    impacts[1].transform.position = hit.point;
                    impacts[1].Play();
                    AI_movement aiMovementScript = hit.collider.GetComponent<AI_movement>();
                    if (aiMovementScript != null)
                    {
                        if (!aiMovementScript.isPlayerSeen)
                        {
                            aiMovementScript.Detection();
                        }
                    }
                     damageScript = hit.collider.transform.parent.parent.parent.parent.GetComponent<EnemyHealth>();
                    if (damageScript != null)
                    {
                        Debug.Log("DamageDealt");
                        damageScript.Damage(10000);
                    }

                }

            }
        }
    }
    public void PickupAmmo()
    {
        bulletCount += 10;
        bulletsString = " " + bulletCount;
        AmmoText.text = bulletsString;
        if(bulletCount>=10)
        {
            AmmoText.color = Color.green;
        }      
    }
}


