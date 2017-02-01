using UnityEngine;
using System.Collections;

public class LaserConsole : MonoBehaviour {

    public GameObject[] lasers;
    public float _health;
    public Material accessGrantedMaterial;

    Transform ConsoleText;
    BoxCollider boxCollider;
    Renderer ren;
    bool hasAccessCard;
    
	void Start ()
    {
        ConsoleText = gameObject.transform.GetChild(0);
        boxCollider = gameObject.transform.parent.gameObject.GetComponent<BoxCollider>();
        ren = GetComponent<Renderer>();
        hasAccessCard = true;
    }
	
    void Update()
    {
        if(_health == 0)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
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
            if(hasAccessCard)
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
