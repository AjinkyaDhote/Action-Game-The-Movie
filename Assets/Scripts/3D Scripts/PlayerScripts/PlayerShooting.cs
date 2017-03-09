using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    private const int AMMO_PICK_UP = 10;
    public int INITIAL_NUMBER_OF_BULLETS = 400;

    private const float PISTOL_RANGE = 5.0f;
    private const int BULLET_COLLISION_LAYER_MASK = 1 << 16;
    
    public const int NUMBER_OF_SHOTGUN_BULLETS = 8;
    private const float SPARY_ANGLE = 1.0f;
    private const float MUZZLE_EFFECT_DISPLAY_TIME = 0.02f;

    private const float WALL_HIT_PREFAB_DESTROY_DELAY = 2.0f;
    private const float WALL_HIT_PREFAB_POSITIONAL_OFFSET = 0.1f;

    //public ParticleSystem muzzleFlash;
    private Animator pistolAnim;
    private Animator shotGunAnim;
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

    public int bulletCount;

    private TimeSlow timeSlowScript;
    //private EnemyThrow enemyThrowScript;
    private float nextFire = 0.0f;
    private WeaponSystem weaponSystemScript;
    private PauseMenu pauseMenuScript;
    private CountdownTimerScript countdownTimer;

    private RaycastHit hit;    
    private GameObject bulletPrefab;
    private List<GameObject> bullets;
    private int bulletInUse = 0;
    private Rigidbody[] shotgunBulletRB;
    private Transform shotgunBulletSpawnerTrasform;

    private Rigidbody pistolBulletRB;
    private Transform pistolBulletSpawnerTrasform;

    private Text AmmoText;
    private Text bulletOverText;
    private GameObject wallHitPrefab;
    private Animation AmmoAnimation;

    void Start()
    {
        timeSlowScript = transform.GetChild(2).FindChild("BulletTime").GetComponent<TimeSlow>();
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
        shotgunBulletRB = new Rigidbody[NUMBER_OF_SHOTGUN_BULLETS];
        weaponSystemScript = GetComponent<WeaponSystem>();
        AmmoText = transform.FindChild("FPS UI Canvas").FindChild("AmmoText").GetComponent<Text>();
        AmmoText.text = " " + bulletCount;
        AmmoText.color = Color.white;
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        countdownTimer = GameObject.FindWithTag("InstructionsCanvas").transform.GetChild(0).GetComponent<CountdownTimerScript>();
        bulletOverText = transform.FindChild("FPS UI Canvas").FindChild("BulletOverText").GetComponent<Text>();
        AmmoAnimation = transform.FindChild("FPS UI Canvas").FindChild("AmmoAnimateText").GetComponent<Animation>();
        AmmoAnimation.gameObject.SetActive(false);
        pistolAnim = transform.GetChild(1).GetComponent<Animator>();
        shotGunAnim = transform.GetChild(0).GetComponent<Animator>();

        GameManager.Instance.headShots = 0;
        GameManager.Instance.totalEnemiesKilled = 0;
        GameManager.Instance.shotsFired = 0;
        GameManager.Instance.bodyShots = 0;
    }
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && (!pauseMenuScript.isPaused) && /*(Time.realtimeSinceStartup > nextFire)*/ (weaponSystemScript.currentWeaponInfo.enableShooting) && (countdownTimer.hasGameStarted))
        {
            GameManager.Instance.shotsFired++;
            //Debug.Log(GameManager.Instance.hitcount);
            if (weaponSystemScript.currentWeaponInHand.Value.name == "ShotGun")
            {
                if (bulletCount >= NUMBER_OF_SHOTGUN_BULLETS)
                {
                    Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, BULLET_COLLISION_LAYER_MASK);
                    bulletCount -= NUMBER_OF_SHOTGUN_BULLETS;
                    nextFire = Time.realtimeSinceStartup + weaponSystemScript.currentWeaponInfo.coolDownTimer;
                    for (int i = 0; i < NUMBER_OF_SHOTGUN_BULLETS; i++)
                    {
                        bullets[bulletInUse].transform.position = shotgunBulletSpawnerTrasform.position;
                        bullets[bulletInUse].transform.rotation = shotgunBulletSpawnerTrasform.rotation * Quaternion.Euler(0.0f, -90.0f, -90.0f);
                        bullets[bulletInUse].SetActive(true);
                        shotGunAnim.SetTrigger("ShotGunShoot");
                        shotgunBulletRB[i] = bullets[bulletInUse].GetComponent<Rigidbody>();
                        if (timeSlowScript.isSlowTimeEnabled)
                        {
                            shotgunBulletRB[i].AddForce(GenerateShotGunSpray(i) * _bulletForce * (1.0f / Time.timeScale) * (0.02f / Time.fixedDeltaTime));
                            //Debug.Log("shot");
                        }
                        else
                        {
                            shotgunBulletRB[i].AddForce(GenerateShotGunSpray(i) * _bulletForce);
                            //Debug.Log("shot");
                        }
                        bullets[bulletInUse].GetComponent<BulletDamage>().IsFired = true;
                        bulletInUse++;
                        SoundManager3D.Instance.shotgun.Play();
                        weaponSystemScript.currentWeaponInfo.muzzleMesh.enabled = true;
                        StartCoroutine(MuzzleEffect());
                    }
                }
                else
                {
                    SoundManager3D.Instance.gunEmpty.Play();
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
                    pistolAnim.SetTrigger("NewGunAnimation");                    
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
                    SoundManager3D.Instance.pistol.Play();
                    weaponSystemScript.currentWeaponInfo.muzzleMesh.enabled = true;
                    StartCoroutine(MuzzleEffect());
                }
                else
                {
                    SoundManager3D.Instance.gunEmpty.Play();
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
        else if (bulletCount < NUMBER_OF_SHOTGUN_BULLETS && !(weaponSystemScript.currentWeaponInHand.Value.name == "Pistol"))
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
    }

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
        AmmoAnimation.gameObject.SetActive(true);
        AmmoAnimation.Play();
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


