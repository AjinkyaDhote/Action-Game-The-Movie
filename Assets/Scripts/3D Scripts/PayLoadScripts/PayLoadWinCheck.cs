using UnityEngine;
using System.Collections;

public class PayLoadWinCheck : MonoBehaviour
{
    WinTrigger winTriggerScript;
    void Start()
    {
        winTriggerScript = GameObject.Find("WinTrigger").GetComponent<WinTrigger>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WinTrigger")
        {
            winTriggerScript.hasPayloadReached = true;
        }
    }
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.name == "WinTrigger")
    //    {
    //        winTriggerScript.didEveryoneReach--;
    //    }
    //}
}
