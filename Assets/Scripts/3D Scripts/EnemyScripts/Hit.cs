using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	private GameObject DestroyedObject;
    private Transform enemySpine;
    private Transform enemyHead;
    private GameObject player;


    void Start()
    {
        DestroyedObject = Resources.Load("CreateDestroyedPrefab/CrateDestroyed") as GameObject;
        enemySpine = transform.parent.parent.parent.parent.parent.parent.parent.parent;
        enemyHead = transform.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(0);
         player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        if ((enemySpine && gameObject.name == "Crate") && enemyHead)
        {
            Physics.IgnoreCollision(enemySpine.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(enemyHead.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
    }



    void OnCollisionEnter( Collision collision ) {

       
        
		DestroyIt();
        Debug.Log(player);
        player.GetComponent<PlayerHealthScript>().PlayerDamage();
		
	}
	
		void DestroyIt(){
		if(DestroyedObject) {
			Instantiate(DestroyedObject, transform.position, transform.rotation);
		}
        //Destroy(gameObject);
        gameObject.SetActive(false);

	}
}