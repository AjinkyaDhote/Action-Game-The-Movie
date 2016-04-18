using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Animator anim;
    public ParticleSystem[] impacts;

    GameObject laserPrefab;
    GameObject laser;

    PauseMenu pauseMenuScript;

    bool shooting = false;
    int bulletCount = 15;

    Text AmmoText;
    string bulletsString;

    void Start()
    {
        AmmoText = transform.FindChild("FPS UI Canvas").FindChild("AmmoText").GetComponent<Text>();
        bulletsString = " " + bulletCount;
        AmmoText.text = bulletsString;
        AmmoText.color = Color.green;
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        anim = GetComponentInChildren<Animator>();
        laserPrefab = Resources.Load("Laser Prefab/Laser") as GameObject;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !pauseMenuScript.isPaused)
        {
            muzzleFlash.Play();
            anim.SetTrigger("Fire");
            shooting = true;
        }
    }


    void FixedUpdate()
    {
        if (shooting && bulletCount > 0)
        {
            bulletCount -= 1;
            bulletsString = " " + bulletCount;
            AmmoText.text = bulletsString;
            if (bulletCount <= 10)
            {
                AmmoText.color = Color.red;
            }
            shooting = false;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            {
                laser = Instantiate(laserPrefab);
                laser.transform.SetParent(transform);
                laser.transform.localPosition = new Vector3(0.827f, -0.896f, 4.481f);
                laser.transform.localRotation = Quaternion.identity;
                laser.GetComponent<Laser>().GetCorrectEnemy(hit.point);

                if (hit.collider.tag == "Wall")
                {
                    impacts[0].transform.position = hit.point;
                    Debug.Log(hit.point);
                    impacts[0].Play();
                }
                else if (hit.collider.GetType() == typeof(CapsuleCollider))
                {

                    impacts[1].transform.position = hit.point;
                    impacts[1].Play();
                    EnemyHealth damageScript = hit.collider.GetComponent<EnemyHealth>();
                    if (damageScript != null)
                    {
                        //damageScript.Damage(damage);
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


