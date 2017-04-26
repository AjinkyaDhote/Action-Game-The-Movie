using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerScript : MonoBehaviour
{
    public GameObject[] lasers;
    Transform ConsoleText;
    //WeaponSystem weaponSystemScript;
    BoxCollider boxCollider;
    Renderer ren;
    public Material accessGrantedMaterial;
           
    void Start()
    {        
       // weaponSystemScript = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).GetComponent<WeaponSystem>();
        ConsoleText = gameObject.transform.parent.GetChild(0);
        boxCollider = transform.parent.parent.GetComponent<BoxCollider>();
        ren = transform.parent.GetComponent<Renderer>();              
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
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
                    ConsoleText.gameObject.SetActive(false);
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
