using UnityEngine;
using System.Collections;

public class WeaponInfo:MonoBehaviour
{
    public float coolDownTimer;
    public float bulletLifeTime;
    public Sprite crossHair;
    public MeshRenderer muzzleMesh;
    public bool enableShooting = true;

    public void SetShooting()
    {
        enableShooting = true;
        Debug.Log("enabled True");
    }

    public void ResetShooting()
    {
        enableShooting = false;
        Debug.Log("enabled False");
    }
}
