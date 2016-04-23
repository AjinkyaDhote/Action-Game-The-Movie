using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	private GameObject DestroyedObject;
    private Transform enemySpine;
    private Transform enemyHead;


    void Start()
    {
        DestroyedObject = Resources.Load("CreateDestroyedPrefab/CrateDestroyed") as GameObject;
        enemySpine = transform.parent.parent.parent.parent.parent.parent.parent.parent;
        enemyHead = transform.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(0);
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
		
	}
	
		void DestroyIt(){
		if(DestroyedObject) {
			Instantiate(DestroyedObject, transform.position, transform.rotation);
		}
        //Destroy(gameObject);
        gameObject.SetActive(false);

	}
}