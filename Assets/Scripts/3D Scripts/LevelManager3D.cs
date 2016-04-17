using UnityEngine;
using System.Collections;

public class LevelManager3D : MonoBehaviour
{
    public Transform BatteryMeshPrefab;
    public Transform AmmoMeshPrefab;

    private float width2DPlane, width3DPlane, height2DPlane, height3DPlane;
    private Vector3 convertPoint(Vector2 relativePoint)
    {
        Vector3 returnVal;
        returnVal.x = (relativePoint.x / width2DPlane) * width3DPlane;
        returnVal.y = 0.0f;
        returnVal.z = (relativePoint.y / height2DPlane) * height3DPlane;
        return returnVal;
    }

    void Awake ()
    {
        width2DPlane = GameManager.Instance.width2DPlane;
        height2DPlane = GameManager.Instance.height2DPlane;
        width3DPlane = GameManager.Instance.width3DPlane;
        height3DPlane = GameManager.Instance.height3DPlane;
        Vector3 worldPos;

        // instantiate Ammo
        for ( int i=0; i< GameManager.Instance.ammoPosList.Count; i++ )
        {
            worldPos = convertPoint( GameManager.Instance.ammoPosList[i] );
            Instantiate( AmmoMeshPrefab, worldPos, Quaternion.identity );
        }

        // instantiate Battery
        for (int i = 0; i < GameManager.Instance.batteryPosList.Count; i++)
        {
            worldPos = convertPoint(GameManager.Instance.batteryPosList[i]);
            Instantiate(BatteryMeshPrefab, worldPos, Quaternion.Euler(-90, 0, 0));
        }
    }
}