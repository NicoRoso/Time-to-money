using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;

    public string primaryName;
    public string secondaryName;

    [SerializeField] private InputActionAsset input;

    private InputAction switchAction;

    void Awake()
    {
        FindWeapons();
        SwitchWeapon(currentWeaponIndex);

        switchAction = input.FindActionMap("Player").FindAction("Switch Weapon");

        switchAction.performed += context => SwitchToNextWeapon();
    }

    private void OnEnable()
    {
        switchAction.Enable();
    }

    private void OnDisable()
    {
        switchAction.Disable();
    }

    void FindWeapons()
    {
        List<GameObject> foundWeapons = new List<GameObject>();

        GameObject primaryWeaponObj = GameObject.Find(primaryName);
        if (primaryWeaponObj != null)
        {
            foundWeapons.Add(primaryWeaponObj);
        }
        else
        {
            Transform primaryTransform = transform.Find(primaryName);
            if (primaryTransform != null)
            {
                foundWeapons.Add(primaryTransform.gameObject);
            }
        }

        GameObject secondaryWeaponObj = GameObject.Find(secondaryName);
        if (secondaryWeaponObj != null)
        {
            foundWeapons.Add(secondaryWeaponObj);
        }
        else
        {
            Transform secondaryTransform = transform.Find(secondaryName);
            if (secondaryTransform != null)
            {
                foundWeapons.Add(secondaryTransform.gameObject);
            }
        }

        weapons = foundWeapons.ToArray();
    }

    void SwitchToNextWeapon()
    {
        if (IsReloading(weapons[currentWeaponIndex]))
        {
            return;
        }

        int nextWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        SwitchWeapon(nextWeaponIndex);
    }

    bool IsReloading(GameObject weaponObj)
    {
        if (weaponObj != null)
        {
            Gun gunComponent = weaponObj.GetComponent<Gun>();
            if (gunComponent != null && gunComponent.isReloading)
            {
                return true;
            }

            Shotgun shotgunComponent = weaponObj.GetComponent<Shotgun>();
            if (shotgunComponent != null && shotgunComponent.isReloading)
            {
                return true;
            }
        }
        return false;
    }

    void SwitchWeapon(int index)
    {
        foreach (GameObject weapon in weapons)
        {
            if (weapon != null)
            {
                weapon.SetActive(false);
            }
        }

        if (weapons[index] != null)
        {
            weapons[index].SetActive(true);
            currentWeaponIndex = index;
        }
    }
}
