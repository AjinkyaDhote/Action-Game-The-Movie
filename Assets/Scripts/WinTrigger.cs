
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
    }   


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("WinTriggerDetectionCollider"))
        {            
            doorAnimationController.SetBool("Open", true);
        }
    }
}
