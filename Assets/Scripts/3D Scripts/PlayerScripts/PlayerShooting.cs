﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    private const int AMMO_PICK_UP = 10;
    private const int INITIAL_NUMBER_OF_BULLETS = 40;

    private const float PISTOL_RANGE = 5.0f;
    private const int BULLET_COLLISION_LAYER_MASK = 1 << 16;

    private const float SPARY_ANGLE = 5.0f;
    private const float MUZZLE_EFFECT_DISPLAY_TIME = 0.02f;

    private const float WALL_HIT_PREFAB_DESTROY_DELAY = 2.0f;
    private const float WALL_HIT_PREFAB_POSITIONAL_OFFSET = 0.1f;

    //public ParticleSystem muzzleFlash;
    //public Animator anim;
    //public ParticleSystem[] impacts;

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
    private TimeSlow timeSlowScript;
    //private GameObject laserPrefab;
    //private GameObject laser;
    private AudioSource noBullets;
    [HideInInspector]
    public AudioSource currentGunAudio;
    //private EnemyThrow enemyThrowScript;
    private float nextFire = 0.0f;
    private WeaponSystem weaponSystemScript;
    private PauseMenu pauseMenuScript;
    private CountdownTimerScript countdownTimer;
    //private EnemyHealth damageScript;
    //private bool shooting = false;

    private RaycastHit hit;
    private int bulletCount = INITIAL_NUMBER_OF_BULLETS;
    private GameObject bulletPrefab;
    private List<GameObject> bullets;
    private int bulletInUse = 0;
    private Rigidbody[] shotgunBulletRB;
    private Transform shotgunBulletSpawnerTrasform;

    private Rigidbody pistolBulletRB;
    private Transform pistolBulletSpawnerTrasform;

    private Text AmmoText;
    private Text bulletOverText;
    //private ParticleSystem wallHitParticleSystem;
    //private AI_movement aiMovementScript;
    private GameObject wallHitPrefab;

    void Start()
    {
        timeSlowScript = transform.GetChild(2).FindChild("BulletTime").GetComponent<TimeSlow>();
        //wallHitParticleSystem = GameObject.Find("WallHit").GetComponent<ParticleSystem>();
        wallHitPrefab = Resources.Load<GameObject>("WallHitPrefab/WallHit");
        shotgunBulletSpawnerTrasform = transform.GetChild(0).GetChild(0);
        pistolBulletSpawnerTrasform = transform.GetChild(1).GetChild(0);
        bulletPrefab = Resources.Load<GameObject>("Bullet Prefab/Bullet");
        bullets = new List<GameObject>(INITIAL_NUMBER_OF_BULLETS);
        for (int i = 0; i < bullets.Capacity; i++)
        {
            bullets.Add(Instantiate(bulletPrefab));
            bullets[i].SetActive(false);
        }
        shotgunBulletRB = new Rigidbody[4];
        noBullets = GetComponent<AudioSource>();
        //laserPrefab = Resources.Load("Laser Prefab/Laser") as GameObject;
        weaponSystemScript = GetComponent<WeaponSystem>();
        AmmoText = transform.FindChild("FPS UI Canvas").FindChild("AmmoText").GetComponent<Text>();
        AmmoText.text = " " + bulletCount;
        AmmoText.color = Color.white;
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();

        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();
        bulletOverText = transform.FindChild("FPS UI Canvas").FindChild("BulletOverText").GetComponent<Text>();
    }
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && (!pauseMenuScript.isPaused) && (Time.realtimeSinceStartup > nextFire) && (countdownTimer.hasGameStarted))
        {

            if (weaponSystemScript.currentWeaponInHand.Value.name == "ShotGun")
            {
                if (bulletCount >= 4)
                {
                    Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, BULLET_COLLISION_LAYER_MASK);
                    bulletCount -= 4;
                    nextFire = Time.realtimeSinceStartup + weaponSystemScript.currentWeaponInfo.coolDownTimer;
                    for (int i = 0; i < 4; i++)
                    {
                        bullets[bulletInUse].transform.position = shotgunBulletSpawnerTrasform.position;
                        bullets[bulletInUse].transform.rotation = shotgunBulletSpawnerTrasform.rotation * Quaternion.Euler(0.0f, -90.0f, -90.0f);
                        bullets[bulletInUse].SetActive(true);
                        shotgunBulletRB[i] = bullets[bulletInUse].GetComponent<Rigidbody>();
                        if (timeSlowScript.isSlowTimeEnabled)
                        {
                            shotgunBulletRB[i].AddForce(GenerateShotGunSpray(i) * _bulletForce * (1.0f / Time.timeScale) * (0.02f / Time.fixedDeltaTime));
                        }
                        else
                        {
                            shotgunBulletRB[i].AddForce(GenerateShotGunSpray(i) * _bulletForce);
                        }
                        bullets[bulletInUse].GetComponent<BulletDamage>().IsFired = true;
                        bulletInUse++;
                        currentGunAudio.Play();
                        weaponSystemScript.currentWeaponInfo.muzzleMesh.enabled = true;
                        StartCoroutine(MuzzleEffect());
                    }
                }
                else
                {
                    noBullets.Play();
                }
            }
            else if (weaponSystemScript.currentWeaponInHand.Value.name == "Pistol")
            {
                if (bulletCount > 0)
                {
                    Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, BULLET_COLLISION_LAYER_MASK);
                    bulletCount--;
                    nextFire = Time.realtimeSinceStartup + weaponSystemScript.currentWeaponInfo.coolDownTimer;
                    bullets[bulletInUse].transform.position = pistolBulletSpawnerTrasform.position;
                    bullets[bulletInUse].transform.rotation = pistolBulletSpawnerTrasform.rotation;
                    bullets[bulletInUse].SetActive(true);
                    pistolBulletRB = bullets[bulletInUse].GetComponent<Rigidbody>();
                    if (timeSlowScript.isSlowTimeEnabled)
                    {
                        pistolBulletRB.AddForce((hit.point - pistolBulletSpawnerTrasform.position).normalized * _bulletForce * (1.0f / Time.timeScale) * (0.02f / Time.fixedDeltaTime));
                    }
                    else
                    {
                        pistolBulletRB.AddForce((hit.point - pistolBulletSpawnerTrasform.position).normalized * _bulletForce);
                    }
                    bullets[bulletInUse].GetComponent<BulletDamage>().IsFired = true;
                    bulletInUse++;
                    currentGunAudio.Play();
                    weaponSystemScript.currentWeaponInfo.muzzleMesh.enabled = true;
                    StartCoroutine(MuzzleEffect());
                }
                else
                {
                    noBullets.Play();
                }
            }
            AmmoText.text = bulletCount.ToString();
        }

        if (bulletCount >= 10)
        {
            AmmoText.color = Color.white;
        }
        else if (bulletCount < 10)
        {
            AmmoText.color = Color.red;
        }


        if (bulletCount <= 0)
        {
            bulletOverText.text = "NO BULLETS";
        }
        else if (bulletCount < 4 && !(weaponSystemScript.currentWeaponInHand.Value.name == "Pistol"))
        {
            bulletOverText.text = "SWITCH TO PISTOL";
        }
        else
        {
            bulletOverText.text = "";
        }
    }
    public void DisplayWallHitPreFab(Vector3 hitPoint, Vector3 hitNormal)
    {
        GameObject wallhit = Instantiate(wallHitPrefab, hitPoint + (WALL_HIT_PREFAB_POSITIONAL_OFFSET * hitNormal), Quaternion.LookRotation(hitNormal)) as GameObject;    
        Destroy(wallhit, WALL_HIT_PREFAB_DESTROY_DELAY);
        //wallHitParticleSystem.transform.position = hitPoint;
        //wallHitParticleSystem.transform.rotation = Quaternion.LookRotation(hitNormal);
        //wallHitParticleSystem.Play();
    }

    /*void FixedUpdate()
    {   
        /*else
        {
            if (weaponSystemScript.currentWeaponInfo.gunName == "ShotGun")
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

            else if (weaponSystemScript.currentWeaponInHand.Value.name == "Pistol")
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

        //if (shooting)// && (shotgunBulletCount > weaponSystemScript.currentWeaponInfo.ammoNeeded - 1))
        //{
            //weaponSystemScript.audioGun.Play();
            ////muzzleFlash.Play();
            //if (weaponSystemScript.currentWeaponInHand.Value.name == "ShotGun")
            //{
            //    //anim.SetTrigger("ShotGun");
            //}
            //else if (weaponSystemScript.currentWeaponInHand.Value.name == "PotatoGun" || weaponSystemScript.currentWeaponInHand.Value.name == "MachineGun")
            //{
            //    //anim.SetTrigger("Fire");
            //}

            //if (bulletCount > 0)
            //{
            //    bulletCount -= weaponSystemScript.currentWeaponInfo.ammoNeeded;
            //    if (bulletCount <= 0)
            //    {
            //        bulletCount = 0;
            //    }
            //    AmmoText.text = " " + shotgunBulletCount;
            //}
            //if (bulletCount <= 10)
            //{

            //}
            //shooting = false;
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
        //}
    }*/
    public void PickupAmmo()
    {
        int currentNumberOfBullets = bullets.Count;
        int futureNumberOfBullets = currentNumberOfBullets + AMMO_PICK_UP;
        for (int i = currentNumberOfBullets; i < futureNumberOfBullets; i++)
        {
            bullets.Add(Instantiate(bulletPrefab));
            bullets[i].SetActive(false);
        }
        bulletCount += AMMO_PICK_UP;
        bulletOverText.text = "";
        AmmoText.text = bulletCount.ToString();
        if (bulletCount >= 10)
        {
            AmmoText.color = Color.white;
        }
    }

    Vector3 GenerateShotGunSpray(int i)
    {
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-SPARY_ANGLE, SPARY_ANGLE), ((i < 2) ? weaponSystemScript.currentWeaponInHand.Value.transform.forward : weaponSystemScript.currentWeaponInHand.Value.transform.up));
        return rotation * (hit.point - shotgunBulletSpawnerTrasform.position).normalized;
    }
    IEnumerator MuzzleEffect()
    {
        yield return new WaitForSeconds(MUZZLE_EFFECT_DISPLAY_TIME);
        weaponSystemScript.currentWeaponInfo.muzzleMesh.enabled = false;
    }
}


