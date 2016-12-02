using UnityEngine;
using System.Collections;

public class PlayerWinCheck : MonoBehaviour
{
    WinTrigger winTriggerScript;
   

    void Start()
    {
        winTriggerScript = GameObject.Find("WinTrigger").GetComponent<WinTrigger>();
     
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name== "WinTrigger")
        {          
            winTriggerScript.hasPlayerReached = true;

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "WinTrigger")
        {
            winTriggerScript.hasPlayerReached = false;
        }
    }
}
