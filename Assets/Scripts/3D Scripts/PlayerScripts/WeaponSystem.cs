using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour
{
    private bool moveForward;
    LinkedList<GameObject> weapons;
    [HideInInspector]
    public WeaponInfo currentWeaponInfo;
    [HideInInspector]
    public LinkedListNode<GameObject> currentWeaponInHand;
    //[HideInInspector]
    //public AudioSource audioGun;

    PlayerShooting playerShootingScript;

    void Start()
    {
        moveForward = false;
        playerShootingScript = GetComponent<PlayerShooting>();
        GameObject[] weaponsGO = GameObject.FindGameObjectsWithTag("Gun");
        foreach (GameObject weapon in weaponsGO)
        {
            weapon.SetActive(false);
        }
        weapons = new LinkedList<GameObject>(weaponsGO);
        currentWeaponInHand = weapons.First;
        currentWeaponInfo = currentWeaponInHand.Value.GetComponent<WeaponInfo>();
        //audioGun = currentWeaponInHand.Value.GetComponent<AudioSource> ();
        UpdateWeaponInHand();
    }

    void Update()
    {
        //if (!(playerShootingScript.anim.GetCurrentAnimatorStateInfo (0).IsName ("ShotGunAnimation")))
        //{
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.Z))
        {
            moveForward = true;
            UpdateWeaponInHand();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.X))
        {
            moveForward = false;
            UpdateWeaponInHand();
        }
        //}
    }
    void UpdateWeaponInHand()
    {
        currentWeaponInHand.Value.SetActive(false);
        if (moveForward)
        {
            currentWeaponInHand = currentWeaponInHand.Next;
            if (currentWeaponInHand != null)
            {
                currentWeaponInHand.Value.SetActive(true);
            }
            else
            {
                currentWeaponInHand = weapons.First;
                currentWeaponInHand.Value.SetActive(true);
            }
        }
        else
        {
            currentWeaponInHand = currentWeaponInHand.Previous;
            if (currentWeaponInHand != null)
            {
                currentWeaponInHand.Value.SetActive(true);
            }
            else
            {
                currentWeaponInHand = weapons.Last;
                currentWeaponInHand.Value.SetActive(true);
            }
        }
        currentWeaponInfo = currentWeaponInHand.Value.GetComponent<WeaponInfo>();
        //ParticleSystem[] pS = currentWeaponInHand.Value.GetComponentsInChildren<ParticleSystem>();
        //audioGun = currentWeaponInHand.Value.GetComponent<AudioSource>();
        //for (int i = 0; i < pS.Length; i++)
        //{
        //    if (pS[i].name == "MuzzleFlash")
        //    {
        //        playerShootingScript.muzzleFlash = pS[i];
        //    }
        //    else if (pS[i].name == "WallCollision")
        //    {
        //        playerShootingScript.impacts[0] = pS[i];
        //    }
        //    else
        //    {
        //        playerShootingScript.impacts[1] = pS[i];
        //    }
        //}
        //playerShootingScript.anim = currentWeaponInHand.Value.GetComponent<Animator>();
    }
}
