using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour {
    Dictionary<string, GameObject> weapons;
    List<string> weaponNames;
    string previousWeapon, currentWeapon, nextWeapon;
    int weaponIndex;
    int weaponCount;
    PlayerShooting playerShootingScript;

    void Start()
    {
        playerShootingScript = GetComponent<PlayerShooting>();
        GameObject[] weaponsGO = GameObject.FindGameObjectsWithTag("Gun");
        weaponNames = new List<string> { "MachineGun", "GravityGun", "ShotGun" };
        weapons = new Dictionary<string, GameObject>();
        for (int i = 0; i < weaponsGO.Length; i++)
        {
            weaponsGO[i].SetActive(false);
            weapons.Add(weaponsGO[i].name, weaponsGO[i]);
        }
        weaponIndex = 0;
        weaponCount = weaponNames.Count - 1;
        previousWeapon = weaponNames[weaponIndex];
        currentWeapon = weaponNames[++weaponIndex];
        nextWeapon = weaponNames[++weaponIndex];
        UpdateWeaponInHand();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.Z))
        {
			currentWeapon = previousWeapon;
			previousWeapon = nextWeapon;

            if (weaponIndex > 0)
            {
                nextWeapon = weaponNames[--weaponIndex];
            }
            else
            {
				weaponIndex = weaponCount;
                nextWeapon = weaponNames[weaponIndex];
            }
            UpdateWeaponInHand();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.X)) 
        {
			previousWeapon = currentWeapon;
            currentWeapon = nextWeapon;
			if (weaponIndex < weaponCount)
            {
                nextWeapon = weaponNames[++weaponIndex];
            }
            else
            {
				weaponIndex = 0;
				nextWeapon = weaponNames[weaponIndex];
            }
            UpdateWeaponInHand();
        }
    }
    void UpdateWeaponInHand()
    {
        foreach (KeyValuePair<string, GameObject> weapon in weapons)
        {
            if (weapon.Key == currentWeapon)
            {
                weapon.Value.SetActive(true);
                ParticleSystem[] pS = weapon.Value.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < pS.Length; i++)
                {
                    if (pS[i].name == "MuzzleFlash")
                    {
                        playerShootingScript.muzzleFlash = pS[i];
                    }
                    else if (pS[i].name == "WallCollision")
                    {
                        playerShootingScript.impacts[0] = pS[i];
                    }
                    else
                    {
                        //playerShootingScript.impacts [1] = pS [j];
                    }
                }
                playerShootingScript.anim = weapon.Value.GetComponent<Animator>();
            }
            else if (weapon.Key == nextWeapon || weapon.Key == previousWeapon)
            {
               weapon.Value.SetActive(false);
            }
        }
    }
}
