using UnityEngine;
using System.Collections;

public class PrefabAnimation : MonoBehaviour {
    PauseMenu pauseMenuScript;
    // Use this for initialization
    void Start () {
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!pauseMenuScript.isPaused)
        {
            if (this.gameObject.tag == "Ammo")
                this.transform.Rotate(0, 2f, 0);
            if (this.gameObject.tag == "Battery")
                this.transform.Rotate(0, 0, 2f);
        }
    }
}
