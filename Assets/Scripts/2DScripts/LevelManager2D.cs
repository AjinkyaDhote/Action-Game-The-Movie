using UnityEngine;
using System.Collections;

public class LevelManager2D : MonoBehaviour
{
    public GameObject BatteryLayer;
    public GameObject AmmoLayer;
    public GameObject map;

	void Awake ()
    {
        MapScript mapScript = map.GetComponent<MapScript>();

        for ( int i = 0; i<AmmoLayer.transform.childCount; i++)
        {
            Vector3 ammoPos = AmmoLayer.transform.GetChild(i).position;
            Vector2 imagePos = mapScript.convertToPixels(ammoPos);
            GameManager.Instance.ammoPosList.Add( imagePos );
        }

	    for ( int i=0; i<BatteryLayer.transform.childCount; i++ )
        {
            Vector3 batteryPos = BatteryLayer.transform.GetChild(i).position;
            Vector2 imagePos = mapScript.convertToPixels( batteryPos );
            GameManager.Instance.batteryPosList.Add( imagePos );
        }

	}
}