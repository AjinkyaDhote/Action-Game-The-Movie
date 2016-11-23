using UnityEngine;
using System.Collections;

public class LaserConsole : MonoBehaviour {

    public GameObject[] lasers;

    Transform ConsoleText;
    BoxCollider boxCollider;
    
	void Start ()
    {
        ConsoleText = gameObject.transform.GetChild(0);
        boxCollider = gameObject.transform.parent.gameObject.GetComponent<BoxCollider>();
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
