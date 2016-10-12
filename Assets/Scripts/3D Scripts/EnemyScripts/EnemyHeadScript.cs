using UnityEngine;
using System.Collections;

public class EnemyHeadScript : MonoBehaviour {
    private EnemyHealth enemyHealth;
    private Rigidbody rBody;
	// Use this for initialization
	void Start () {
        enemyHealth = GameObject.Find("WhiteBoxEnemy").GetComponent<EnemyHealth>();
        rBody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	public void HeadFall () {
        if(enemyHealth.currentHealth == 0)
        {
            transform.parent = null;
            rBody.useGravity = true;
            
        }
	
	}
}
