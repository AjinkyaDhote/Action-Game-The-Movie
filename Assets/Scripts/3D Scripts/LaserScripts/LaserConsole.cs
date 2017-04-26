using UnityEngine;
using System.Collections;

public class LaserConsole : MonoBehaviour
{
    public float _health;
    GameObject brokenLaserConsole;
    Transform consoleTransform;

    void Start()
    {
        brokenLaserConsole = Resources.Load<GameObject>("BrokenConsole/BrokenLaserConsole");
        consoleTransform = gameObject.transform;
    }


    void Update()
    {
        //Physics.IgnoreCollision()

        if (_health <= 0)
        {
            transform.parent.gameObject.SetActive(false);
            //brokenLaserConsole.parent = null;
            Instantiate(brokenLaserConsole, consoleTransform.position - new Vector3(0f, 2f, 0f), consoleTransform.rotation);
            //brokenLaserConsole.gameObject.SetActive(true);
        }
        //Debug.Log(LevelManager3D.accessCardCount);
    }

}



