using UnityEngine;
using System.Collections;

public class Level3DScript : MonoBehaviour
{
	void Awake ()
    {
        Vector3 size = gameObject.GetComponent<MeshRenderer>().bounds.size;
        GameManager.Instance.width3DPlane = size.x;
        GameManager.Instance.height3DPlane = size.z;
    }
}
