using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour {
    List<GameObject> weaponsGOList;
    List<string> weaponNames;
    string previousWeapon, currentWeapon, nextWeapon;
    int weaponIndex;
    int weaponCount;
    PlayerShooting playerShootingScript;

    void Start()
    {
        playerShootingScript = GetComponent<PlayerShooting>();
        GameObject[] weaponsGO = GameObject.FindGameObjectsWithTag("Gun");
        weaponsGOList = new List<GameObject>();
        for (int i = 0; i < weaponsGO.Length; i++)
        {
            weaponsGO[i].SetActive(false);
            weaponsGOList.Add(weaponsGO[i]);
        }
        weaponIndex = 0;
        weaponNames = new List<string> { "MachineGun", "GravityGun", "ShotGun" };
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
        for (int i = 0; i < weaponsGOList.Count; i++)
        {
            if (weaponsGOList[i].name == currentWeapon)
            {
                weaponsGOList[i].SetActive(true);
				ParticleSystem[] pS = weaponsGOList [i].GetComponentsInChildren<ParticleSystem> ();
				for (int j = 0; j < pS.Length; j++) {
					if (pS [j].name == "MuzzleFlash") {
						playerShootingScript.muzzleFlash = pS [j];
					} else if (pS [j].name == "WallCollision") {
						playerShootingScript.impacts [0] = pS [j];
					} else {
						//playerShootingScript.impacts [1] = pS [j];
					}
				}
				/*playerShootingScript.muzzleFlash = weaponsGOList [i].GetComponentInChildren<ParticleSystem> ();
				Debug.Log (weaponsGOList [i].GetComponentInChildren<ParticleSystem> ().name );
                playerShootingScript.anim = weaponsGOList[i].GetComponent<Animator>();
				playerShootingScript.impacts[0] = weaponsGOList[i].GetComponentInChildren<ParticleSystem> ();
				//playerShootingScript.impacts[1] = weaponsGOList[i].GetComponentInChildren<ParticleSystem>();*/
            }
            else if (weaponsGOList[i].name == nextWeapon || weaponsGOList[i].name == previousWeapon)
            {
                weaponsGOList[i].SetActive(false);
            }
        }
    }
}
