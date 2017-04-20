    using UnityEngine;
using System.Collections;

public class LaserConsole : MonoBehaviour {

    public GameObject[] lasers;
    public float _health;
    public Material accessGrantedMaterial;

    WeaponSystem weaponSystemScript;
    Transform ConsoleText;
    BoxCollider boxCollider;
    Renderer ren;
    GameObject brokenLaserConsole;
    Transform consoleTransform;
    
	void Start ()
    {
        ConsoleText = gameObject.transform.GetChild(0);
        boxCollider = gameObject.transform.parent.gameObject.GetComponent<BoxCollider>();
        brokenLaserConsole = Resources.Load<GameObject>("BrokenConsole/BrokenLaserConsole");
        ren = GetComponent<Renderer>();
        consoleTransform = gameObject.transform;
        weaponSystemScript = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).GetComponent<WeaponSystem>();
    }
	
    void Update()
    {
        if(_health <= 0)
        {
            transform.parent.gameObject.SetActive(false);
            //brokenLaserConsole.parent = null;
            Instantiate(brokenLaserConsole, consoleTransform.position - new Vector3(0f, 2f, 0f), consoleTransform.rotation);
            //brokenLaserConsole.gameObject.SetActive(true);
        }
        //Debug.Log(LevelManager3D.accessCardCount);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            _health -= 1f;
            Destroy(other.gameObject);
        }

        if (weaponSystemScript.currentWeaponInHand.Value.name == "ShotGun")
        {
            GameManager.Instance.bodyShots += 1 / (float)PlayerShooting.NUMBER_OF_SHOTGUN_BULLETS;
        }

        else if (weaponSystemScript.currentWeaponInHand.Value.name == "Pistol")
        {
            GameManager.Instance.bodyShots++;
        }
                                   
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"));
        {
            if (LevelManager3D.accessCardCount > 0)
            {
                ConsoleText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ren.material = accessGrantedMaterial;

                    foreach (GameObject laser in lasers)
                    {
                        laser.SetActive(false);
                    }
                    boxCollider.enabled = false;
                    LevelManager3D.accessCardCount--;
                    AccessCardCanvas.UpdateNumberOfCards();
                }
            }            
        }        
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ConsoleText.gameObject.SetActive(false);
        }
    }
}
