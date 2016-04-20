using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public class MouseLook
    {
        [Range(0.1f, 2.0f)]
        public float XSensitivity = 1.0f;
        [Range(0.1f, 2.0f)]
        public float YSensitivity = 1.0f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90.0F;
        public float MaximumX = 90.0F;
        public bool smooth;
        [Range(1.0f, 10.0f)]
        public float smoothTime = 5.0f;
        //public bool lockCursor = true;

        Quaternion m_CharacterTargetRot;
        Quaternion m_CameraTargetRot;
        //bool m_cursorIsLocked = true;

        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;

            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            if (smooth)
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot, smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            //UpdateCursorLock();
        }

        /*void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        void InternalLockUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                m_cursorIsLocked = false;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
				Cursor.lockState = CursorLockMode.Locked; 
				Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
            }
        }*/

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
    public Camera mainCamera;
    public int playerSpeed;
    Vector3[] wayPoints3D;
    Rigidbody rigidBody;
    int wayPointNumber;
    public MouseLook mouseLook;
	wasdMovement WASDmovement;
    PauseMenu pauseMenuScript;

    float width2DPlane, width3DPlane, height2DPlane, height3DPlane;
    Vector3 convertPoint(Vector2 relativePoint)
    {
        Vector3 returnVal;
        returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
        returnVal.y = 1.0f;
        returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
        return returnVal;
    }

    void Start()
    {
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();

        width2DPlane = GameManager.Instance.width2DPlane;
        height2DPlane = GameManager.Instance.height2DPlane;
        width3DPlane = GameManager.Instance.width3DPlane;
        height3DPlane = GameManager.Instance.height3DPlane;

        WASDmovement = GetComponent<wasdMovement> ();
        wayPointNumber = 1;
        rigidBody = GetComponent<Rigidbody>();
		if (!WASDmovement.enabled)
        {
            wayPoints3D = new Vector3[GameManager.Instance.mapPoints.Count];
            for (int i = 0; i < wayPoints3D.Length; i++)
            {
                wayPoints3D[i] = convertPoint(GameManager.Instance.mapPoints[i]);
            }
            transform.position = wayPoints3D[0];
        }  
        mouseLook = new MouseLook();
        
    }
    void Update()
    {
        if(!pauseMenuScript.isPaused)
        {
            mouseLook.LookRotation(transform, mainCamera.transform);
        } 
    }

    void FixedUpdate()
    {
		if (!WASDmovement.enabled) {
			if (Vector3.Distance(transform.position, wayPoints3D[wayPointNumber]) < 0.5f)
			{
				if (wayPointNumber < (wayPoints3D.Length - 1))
				{
					wayPointNumber++;
				}
			}
			else
			{
				rigidBody.MovePosition(transform.position + (wayPoints3D[wayPointNumber] - transform.position).normalized * playerSpeed * Time.deltaTime);
			}
		}      
    }
}