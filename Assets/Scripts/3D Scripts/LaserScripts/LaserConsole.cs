using UnityEngine;
using System.Collections;

public class LaserConsole : MonoBehaviour {

    public GameObject[] lasers;
    public float _health;

    Transform ConsoleText;
    BoxCollider boxCollider;
    
	void Start ()
    {
        ConsoleText = gameObject.transform.GetChild(0);
        boxCollider = gameObject.transform.parent.gameObject.GetComponent<BoxCollider>();
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
            ConsoleText.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                for(int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].SetActive(false);
                }

                boxCollider.enabled = false;                                
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
