using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {


	public ParticleSystem muzzleFlash; //MuzzleFlash Particle System
	public Animator anim;
	public ParticleSystem[] impacts;
	public int damage = 1;
    private GameObject laserPrefab;
    private GameObject laser;

    //public int bulletSpeed;
    PauseMenu pauseMenuScript;
    PlayerWithEnemy pScript;
	bool shooting = false;
    private int bulletCount = 0;
	//GameObject bulletPrefab;
	//GameObject bullet;

	//Camera cam;

     

	// Use this for initialization
	void Start () {
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        pScript = transform.parent.parent.GetComponent<PlayerWithEnemy>();
		anim = GetComponentInChildren<Animator> ();
        //cam = GetComponentInParent<Camera>();
        //bulletPrefab = (GameObject)Resources.Load("Bullet Prefab/Bullet");

        laserPrefab = Resources.Load("Laser Prefab/Laser") as GameObject;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetButtonDown ("Fire1") && !pauseMenuScript.isPaused) 
		{
			muzzleFlash.Play ();
			anim.SetTrigger ("Fire");
			shooting = true;
		}
        bulletCount = pScript.bulletCount;
			
//		if (bullet) 
//		{
//			bullet.transform.Translate(bullet.transform.up * bulletSpeed * Time.deltaTime);
//		}
	}


	void FixedUpdate()
	{
		if (shooting && bulletCount >0) 
		{
			shooting = false;
			RaycastHit hit;

			if(Physics.Raycast(transform.position,transform.forward,out hit, 50f))
			{

                //Debug.Log ("Shooting");
                //bullet = (GameObject)Instantiate (bulletPrefab);
                //bullet.transform.SetParent (transform);
                //bullet.transform.localPosition = new Vector3 ((0.7f), (0.7f), (3.38f));
                //bullet.transform.localRotation= Quaternion.Euler(90f,0.0f,0.0f);
                //bullet.GetComponent<BulletScript> ().GetCorrectEnemy (hit.transform);


                laser = Instantiate(laserPrefab);
                laser.transform.SetParent(transform);
                laser.transform.localPosition = new Vector3(0.827f, -0.896f, 4.481f);
                laser.transform.localRotation = Quaternion.identity;
                laser.GetComponent<Laser>().GetCorrectEnemy(hit.point);



                if (hit.collider.tag == "Wall") {
					impacts [0].transform.position = hit.point;
					Debug.Log (hit.point);
					impacts [0].Play ();
				}

				else if (hit.collider.GetType() == typeof(CapsuleCollider)) 
				{
				
					impacts [1].transform.position = hit.point;
					impacts [1].Play ();
					EnemyHealth damageScript = hit.collider.GetComponent<EnemyHealth> ();
					//Debug.Log ("EnemyHit");
					if (damageScript != null) 
					{
						damageScript.Damage (damage);
					}
				
				}


//				impacts .transform.position = hit.point;
//				impacts [currentImpact].GetComponent<ParticleSystem> ().Play ();
//				if (++currentImpact >= maxImpacts)
//					currentImpact = 0;
			}
		
		}
	}
}
