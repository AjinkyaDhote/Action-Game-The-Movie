using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBlast : MonoBehaviour
{
    private GameObject droneBlastPrefab;
    private EnemyHealth enemyHealth;
    private bool isBlastPlayed;
	void Start ()
    {
        droneBlastPrefab = Resources.Load<GameObject>("DroneBlastPrefab/DroneBlast");
        enemyHealth = GetComponent<EnemyHealth>();
        isBlastPlayed = false;
	}
	
	
	void Update ()
    {
		if(enemyHealth.IsKilled && !isBlastPlayed)
        {
            PlayBlastEffect();
        }
	}

    public void PlayBlastEffect()
    {
        isBlastPlayed = true;
        GameObject blast = Instantiate(droneBlastPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        Destroy(blast, 1f); 
    }
}
