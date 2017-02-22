using UnityEngine;
using System.Collections;

public class LaserConsole : MonoBehaviour {

    public GameObject[] lasers;
    public float _health;
    public Material accessGrantedMaterial;

    Transform ConsoleText;
    BoxCollider boxCollider;
    Renderer ren;
    Transform brokenLaserConsole;    
    
	void Start ()
    {
        ConsoleText = gameObject.transform.GetChild(0);
        boxCollider = gameObject.transform.parent.gameObject.GetComponent<BoxCollider>();
        brokenLaserConsole = transform.parent.GetChild(6);
        ren = GetComponent<Renderer>();        
    }
	
    void Update()
    {
        if(_health <= 0)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            brokenLaserConsole.parent = null;
            brokenLaserConsole.gameObject.SetActive(true);
        }
        Debug.Log(LevelManager3D.accessCardCount);
    }


    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.layer == 17  )
        {
            _health -= 1f;
        }
    }    
        	    	

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(LevelManager3D.accessCardCount != 0)
            {
                ConsoleText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ren.material = accessGrantedMaterial;

                    for (int i = 0; i < lasers.Length; i++)
                    {
                        lasers[i].SetActive(false);
                    }
                    boxCollider.enabled = false;
                    LevelManager3D.accessCardCount--;                                      
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
