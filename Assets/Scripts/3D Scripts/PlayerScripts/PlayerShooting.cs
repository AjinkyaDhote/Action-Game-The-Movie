using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    private const int AMMO_PICK_UP = 10;
    private const int INITIAL_NUMBER_OF_BULLETS = 5;

    public ParticleSystem muzzleFlash;
    public Animator anim;
    public ParticleSystem[] impacts;
    

    [SerializeField]
    float _bulletForce = 0.0f;
    public float BulletForce
    {
        get
        {
            return _bulletForce;
        }
        set
        {
            _bulletForce = value;
        }
    }

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
    GameObject bulletPrefab;
    List<GameObject> bullets;
    int bulletInUse;
    Rigidbody bulletRB;
    Transform bulletSpawnerTrasform;

    Text AmmoText;
    string bulletsString;
    Text bulletOverText;

    AI_movement aiMovementScript;
    EnemyThrow enemyThrowScript;

    void Start()
    {
        bulletInUse = 0;
        bulletCount = INITIAL_NUMBER_OF_BULLETS;
        bulletSpawnerTrasform = transform.GetChild(0).GetChild(0);
        bulletPrefab = Resources.Load("Bullet Prefab/Bullet") as GameObject;
        bullets = new List<GameObject>(bulletCount);
        for (int i = 0; i < bullets.Capacity; i++)
        {
            bullets.Add(Instantiate(bulletPrefab) as GameObject);
            bullets[i].SetActive(false);
        }
        noBullets = GetComponent<AudioSource>();
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
                if (bulletCount <= weaponSystemScript.currentWeaponInfo.ammoNeeded - 1)
                {
                    noBullets.Play();
                }
            }
        }
        else if (Input.GetButtonDown("Fire1") && 
                (!pauseMenuScript.isPaused) && 
                (Time.time > nextFire) && 
                (countdownTimer.hasGameStarted) && 
                (bulletCount != 0))
        {
            nextFire = Time.time + weaponSystemScript.currentWeaponInfo.coolDownTimer;
            shooting = true;
            bullets[bulletInUse].transform.position = bulletSpawnerTrasform.position;
            bullets[bulletInUse].SetActive(true);
            bulletRB = bullets[bulletInUse].GetComponent<Rigidbody>();
            bulletRB.AddForce(-bulletSpawnerTrasform.up * _bulletForce);
            bulletInUse++;
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

            else if (weaponSystemScript.currentWeaponInHand.Value.name == "PotatoGun")
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
            else if (weaponSystemScript.currentWeaponInHand.Value.name == "PotatoGun" || weaponSystemScript.currentWeaponInHand.Value.name == "MachineGun")
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
            //RaycastHit hit;

            //if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            //{
            //    if (weaponSystemScript.currentWeaponInHand.Value.name == "GravityGun")
            //    {
            //        laser = Instantiate(laserPrefab);
            //        laser.transform.SetParent(transform);
            //        laser.transform.localPosition = new Vector3(0.695f, -0.809f, 2.805f);
            //        laser.transform.localRotation = Quaternion.identity;
            //        laser.GetComponent<Laser>().GetCorrectEnemy(hit.point);
            //    }
            //    if (hit.collider.tag == "Wall")
            //    {
            //        impacts[0].transform.position = hit.point;
            //        Debug.Log(hit.point);
            //        impacts[0].Play();
            //    }
            //    else if (hit.collider.tag == "BodyCollider")
            //    {

            //        impacts[1].transform.position = hit.point;
            //        impacts[1].Play();
            //        if(hit.collider.transform.CompareTag("SmallEnemy"))
            //        {
            //            aiMovementScript = hit.collider.transform.GetComponentInParent<AI_movement>();
            //            if (!aiMovementScript.isPlayerSeenA)
            //            {
            //                aiMovementScript.Detection();
            //            }
            //        }
            //        if(hit.collider.transform.parent.parent.CompareTag("BigEnemy"))
            //        {
            //            enemyThrowScript = hit.collider.transform.parent.parent.GetComponent<EnemyThrow>();
            //            if (!enemyThrowScript.isPlayerSeen)
            //            {
            //                enemyThrowScript.Detection();
            //            }
            //        }               
            //        damageScript = hit.collider.GetComponentInParent<EnemyHealth>();
            //        if ((damageScript != null) && !damageScript.isKilled)
            //        {
            //            damageScript.Damage(weaponSystemScript.currentWeaponInfo.damageDealt);
            //        }
            //    }
            //    else if ((hit.collider.tag == "HeadCollider"))
            //    {
            //        impacts[1].transform.position = hit.point;
            //        impacts[1].Play();
            //        damageScript = hit.collider.transform.GetComponentInParent<EnemyHealth>();
            //        if ((damageScript != null) && (!damageScript.isKilled))
            //        {
            //            damageScript.Damage(25);
            //            GameManager.Instance.headShots++;
            //            Debug.Log("Head Shot");
            //        }
            //    }
            //}
        }
    }
    public void PickupAmmo()
    {
        int currentNumberOfBullets = bullets.Capacity;
        int futureNumberOfBullets = currentNumberOfBullets + AMMO_PICK_UP;
        for (int i = currentNumberOfBullets; i < futureNumberOfBullets; i++)
        {
            bullets.Add(Instantiate(bulletPrefab) as GameObject);
            bullets[i].SetActive(false);
        }
        bulletCount += AMMO_PICK_UP;
        bulletOverText.text = "";
        bulletsString = " " + bulletCount;
        AmmoText.text = bulletsString;
        if (bulletCount >= AMMO_PICK_UP)
        {
            AmmoText.color = Color.white;
        }
    }
}


