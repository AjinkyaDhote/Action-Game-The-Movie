using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    private bool moveForward;
    LinkedList<GameObject> weapons;
    [HideInInspector]
    public WeaponInfo currentWeaponInfo;
    [HideInInspector]
    public LinkedListNode<GameObject> currentWeaponInHand;
    private Image crossHair;

    void Start()
    {
        moveForward = false;
        crossHair = transform.FindChild("FPS UI Canvas").FindChild("CrossHair").GetComponent<Image>();
        GameObject[] weaponsGO = GameObject.FindGameObjectsWithTag("Gun");
        foreach (GameObject weapon in weaponsGO)
        {
            weapon.SetActive(false);
        }
        weapons = new LinkedList<GameObject>(weaponsGO);
        currentWeaponInHand = weapons.First;
        currentWeaponInfo = currentWeaponInHand.Value.GetComponent<WeaponInfo>();
        UpdateWeaponInHand();
    }

    void Update()
    {
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
        crossHair.sprite = currentWeaponInfo.crossHair;
    }
}
