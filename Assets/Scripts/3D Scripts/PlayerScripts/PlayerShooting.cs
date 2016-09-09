using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    EnemyHealth damageScript;
    bool shooting = false;
    int bulletCount;
    AudioSource noBullets;
    //GameObject bulletPrefab;
    GameObject[] bullets;
    int bulletInUse = 0;

    Text AmmoText;
    string bulletsString;
    Text bulletOverText;

    AI_movement aiMovementScript;
    EnemyThrow enemyThrowScript;

    void Start()
    {
        //bulletPrefab = Resources.Load("Bullet Prefab/Bullet") as GameObject;
        //bullets = new GameObject[GameManager.Instance.totalAmmoCollected];
        //for (int i = 0; i < bullets.Length; i++)
        //{
        //    //bullets[i] = Instantiate(bulletPrefab, transform.GetChild(0)) as GameObject;
        //    bullets[i].SetActive(false);
        //}
        noBullets = GetComponent<AudioSource>();
        bulletCount = 75;
        weaponSystemScript = GetComponent<WeaponSystem>();
        AmmoText = transform.FindChild("FPS UI Canvas").FindChild("AmmoText").GetComponent<Text>();
        bulletsString = " " + bulletCount;
        AmmoText.text = bulletsString;
		AmmoText.color = Color.white;
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        laserPrefab = Resources.Load("Laser Prefab/Laser") as GameObject;
        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();
        bulletOverText = transform.FindChild("FPS UI Canvas").FindChild("BulletOverText").GetComponent<Text>();
    }
    void Update()
    {
        if (weaponSystemScript.currentWeaponInHand.Value.name == "MachineGun")
        {
            if (Input.GetButton("Fire1") && !pauseMenuScript.isPaused && Time.time > nextFire && countdownTimer.hasGameStarted)
            {
                nextFire = Time.time + weaponSystemScript.currentWeaponInfo.coolDownTimer;
                shooting = true;
                bullets[bulletInUse].transform.localPosition = transform.localPosition + new Vector3(0.734f, -0.7040001f, 3.542001f);
                bullets[bulletInUse++].SetActive(true);
                if (bulletCount <= weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
                {
                    noBullets.Play();
                }
            }
        }
        else if (Input.GetButtonDown("Fire1") && !pauseMenuScript.isPaused && Time.time > nextFire && countdownTimer.hasGameStarted)
        {
            nextFire = Time.time + weaponSystemScript.currentWeaponInfo.coolDownTimer;
            shooting = true;
            if (bulletCount <= weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
            {
                noBullets.Play();
            }
        }
    }
    void FixedUpdate()
    {
        if (bulletCount <= 0)
        {
            bulletOverText.text = "OUT OF AMMO";
        }
        else
        {
            if (weaponSystemScript.currentWeaponInHand.Value.name == "ShotGun")
            {
                if (bulletCount <= weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
                {
                    bulletOverText.text = "SWITCH GUN";
                }
                else if (bulletCount > weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
                {
                    bulletOverText.text = "";
                }
            }

            else if (weaponSystemScript.currentWeaponInHand.Value.name == "GravityGun")
            {
                if (bulletCount <= weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
                {
                    bulletOverText.text = "SWITCH GUN";
                }
                else if (bulletCount > weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
                {
                    bulletOverText.text = "";
                }
            }
            else if (weaponSystemScript.currentWeaponInHand.Value.name == "MachineGun")
            {
                if (bulletCount > weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
                {
                    bulletOverText.text = "";
                }
            }
        }

        if (shooting && (bulletCount > weaponSystemScript.currentWeaponInfo.ammoNeeded - 1))
        {
            weaponSystemScript.audioGun.Play();
            muzzleFlash.Play();
            if (weaponSystemScript.currentWeaponInHand.Value.name == "ShotGun")
            {
                anim.SetTrigger("ShotGun");
            }
            else if (weaponSystemScript.currentWeaponInHand.Value.name == "GravityGun" || weaponSystemScript.currentWeaponInHand.Value.name == "MachineGun")
            {
                anim.SetTrigger("Fire");
            }

            if (bulletCount > 0)
            {
                bulletCount -= weaponSystemScript.currentWeaponInfo.ammoNeeded;
                if (bulletCount <= 0)
                {
                    bulletCount = 0;
                }
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
                    if(hit.collider.transform.parent.parent.CompareTag("SmallEnemy"))
                    {
                        aiMovementScript = hit.collider.transform.parent.parent.GetComponent<AI_movement>();
                        if (!aiMovementScript.isPlayerSeen)
                        {
                            aiMovementScript.Detection();
                        }
                    }
                    if(hit.collider.transform.parent.parent.CompareTag("BigEnemy"))
                    {
                        enemyThrowScript = hit.collider.transform.parent.parent.GetComponent<EnemyThrow>();
                        if (!enemyThrowScript.isPlayerSeen)
                        {
                            enemyThrowScript.Detection();
                        }
                    }               
                    damageScript = hit.collider.GetComponent<EnemyHealth>();
                    if ((damageScript != null) && !damageScript.isKilled)
                    {
                        damageScript.Damage(weaponSystemScript.currentWeaponInfo.damageDealt);
                    }
                }
                else if ((hit.collider.tag == "HeadCollider"))
                {
                    impacts[1].transform.position = hit.point;
                    impacts[1].Play();
                    damageScript = hit.collider.transform.parent.parent.parent.parent.GetComponent<EnemyHealth>();
                    if ((damageScript != null) && (!damageScript.isKilled))
                    {
                        damageScript.Damage(25);
                        GameManager.Instance.headShots++;
                        Debug.Log("Head Shot");
                    }
                }
            }
        }
    }
    public void PickupAmmo()
    {
        bulletCount += 10;
        bulletOverText.text = "";
        bulletsString = " " + bulletCount;
        AmmoText.text = bulletsString;
        if (bulletCount >= 10)
        {
			AmmoText.color = Color.white;
        }
    }
}


