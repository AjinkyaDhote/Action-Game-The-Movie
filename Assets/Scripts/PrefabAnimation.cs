using UnityEngine;
using System.Collections;

public class PrefabAnimation : MonoBehaviour {
    PauseMenu pauseMenuScript;
    // Use this for initialization
    void Start () {
        pauseMenuScript = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
        if (gameObject.tag == "Battery")
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
	
	// Update is called once per frame
	void Update () {
        if (!pauseMenuScript.isPaused)
        {
            if (gameObject.tag == "Ammo")
                transform.Rotate(0, 2f, 0f);
            if (gameObject.tag == "Battery")
                transform.Rotate(transform.right, 2f, Space.World);
            if(gameObject.tag == "AccessCard")
                transform.Rotate(2f, 0, 0);
        }
    }
}
