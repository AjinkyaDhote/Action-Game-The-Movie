using UnityEngine;
using System.Collections;

public class WeaponInfo:MonoBehaviour
{
    public float coolDownTimer;
    public float bulletLifeTime;
    public Sprite crossHair;
    public MeshRenderer muzzleMesh;

    public bool enableShooting;
    private void Awake()
    {
        enableShooting = true;
    }
    public void ToggleShooting()
    {
        enableShooting = !enableShooting;
        //Debug.Log(enableShooting);
    }
}
