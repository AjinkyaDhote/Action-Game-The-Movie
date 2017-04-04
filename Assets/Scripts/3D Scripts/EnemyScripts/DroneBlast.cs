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
        droneBlastPrefab = Resources.Load<GameObject>("DroneBlastPrefab/DroneBlast_2");
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
        SoundManager3D.Instance.droneBlast.Play();
        GameObject blast = Instantiate(droneBlastPrefab, gameObject.transform.position, (gameObject.transform.rotation)  * Quaternion.Euler(220.0f, 0.0f, 0.0f)) as GameObject;
        Destroy(blast, 1f); 
    }
}
