using UnityEngine;
using System.Collections;

public class PayLoadWinCheck : MonoBehaviour
{
    public bool hasPlayerReached;           

    void Start()
    {
        hasPlayerReached = false;
    }
  

    void OnCollisionEnter(Collision other)
    {

    }  
}
