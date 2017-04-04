    using UnityEngine;
using System.Collections;

public class LaserConsole : MonoBehaviour {

    public GameObject[] lasers;
    public float _health;
    public Material accessGrantedMaterial;

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
       if(other.gameObject.layer == 17)
        {
            _health -= 1f;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (LevelManager3D.accessCardCount == 0) return;
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


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ConsoleText.gameObject.SetActive(false);
        }
    }
}
