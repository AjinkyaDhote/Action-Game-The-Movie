using UnityEngine;
using System.Collections;

public class HeadBob : MonoBehaviour
{
    public Camera Camera;
    public CurveControlledBob motionBob = new CurveControlledBob();
    //public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();
    //public RigidbodyFirstPersonController rigidbodyFirstPersonController;
    public float StrideInterval;
    [Range(0f, 1f)]
    public float RunningStrideLengthen;
    public float BobSpeed = 5.0f;
    private wasdMovement wasd;
    // private CameraRefocus m_CameraRefocus;
    //private bool m_PreviouslyGrounded;
    //private Vector3 m_OriginalCameraPosition;


    void Start()
    {
        motionBob.Setup(Camera, StrideInterval);
        wasd = GetComponent<wasdMovement>();
        //m_OriginalCameraPosition = Camera.transform.localPosition;
        //     m_CameraRefocus = new CameraRefocus(Camera, transform.root.transform, Camera.transform.localPosition);
    }


    void Update()
    {
        if (wasd.isMoving)
        {
            //  m_CameraRefocus.GetFocusPoint();
            Vector3 newCameraPosition;
            //if (rigidbodyFirstPersonController.Velocity.magnitude > 0 && rigidbodyFirstPersonController.Grounded)
            //{
            Camera.transform.localPosition = motionBob.DoHeadBob(BobSpeed);//rigidbodyFirstPersonController.Velocity.magnitude * (rigidbodyFirstPersonController.Running ? RunningStrideLengthen : 1f));
            newCameraPosition = Camera.transform.localPosition;
            newCameraPosition.y = Camera.transform.localPosition.y;// - jumpAndLandingBob.Offset();
                                                                   //}
                                                                   //else
                                                                   //{
                                                                   //    newCameraPosition = Camera.transform.localPosition;
                                                                   //    newCameraPosition.y = m_OriginalCameraPosition.y;// - jumpAndLandingBob.Offset();
                                                                   //}
            Camera.transform.localPosition = newCameraPosition;

            //if (!m_PreviouslyGrounded && rigidbodyFirstPersonController.Grounded)
            //{
            //    StartCoroutine(jumpAndLandingBob.DoBobCycle());
            //}

            //m_PreviouslyGrounded = rigidbodyFirstPersonController.Grounded;
            //  m_CameraRefocus.SetFocusPoint();
        }
    }
}
