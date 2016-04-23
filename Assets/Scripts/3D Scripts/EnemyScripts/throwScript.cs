using UnityEngine;
using System.Collections;

public class throwScript : MonoBehaviour {

    // Use this for initialization
    //rivate Transform righthandTransform;
    private Rigidbody rigid;
    public float crateSpeed;
    private GameObject player;

    void Start () {
        rigid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        //righthandTransform = GameObject.Find("")
        //transform.parent = righthandTransform;
        rigid.useGravity = false;
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void release()
    {
        Debug.Log("crate released");
        transform.parent = null;
        rigid.useGravity = true;
        
        rigid.MovePosition(transform.position + (player.transform.position - transform.position).normalized * crateSpeed * Time.deltaTime);
    }
}
