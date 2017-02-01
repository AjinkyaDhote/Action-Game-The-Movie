using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {

    public Transform endState;
    
    GameObject payload;    
    bool isDoorOpen;

    // Use this for initialization
    void Start ()
    {        
        payload = GameObject.FindGameObjectWithTag("NewPayload");        
        isDoorOpen = false;
    }
   
    void Update()
    {
        if(isDoorOpen)
        {
            payload.transform.forward = Vector3.Lerp(payload.transform.forward, (endState.transform.position - payload.transform.position), Time.deltaTime * 0.2f);             
            payload.transform.Translate((endState.transform.position - payload.transform.position).normalized * 5f * Time.deltaTime, Space.World);
        }
    }  

    public void SetDoorOpen()
    {
        isDoorOpen = true;
    }
}
