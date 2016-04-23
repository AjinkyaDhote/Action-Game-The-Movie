using UnityEngine;
using System.Collections;

public class PrefabAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.tag == "Ammo")
            this.transform.Rotate(0, 2f,0);
        if (this.gameObject.tag == "Battery")
            this.transform.Rotate(0, 0, 2f);
    }
}
