
using UnityEngine;

public class WinTrigger : MonoBehaviour {

    //public Scoring m_scoring;
   
    [HideInInspector]
    public bool hasPlayerReached = false;
    [HideInInspector]
    public bool hasPayloadReached = false;

    private Animator doorAnimationController;
    void Start()
    {
        doorAnimationController = transform.GetChild(1).GetComponent<Animator>();
       
    }
    void Update()
    {      
        if (hasPlayerReached  && hasPayloadReached)
        {
            doorAnimationController.SetBool("Open",true);  
        }
    } 
}
