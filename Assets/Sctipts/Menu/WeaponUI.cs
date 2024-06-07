using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoCount;

    [SerializeField] private Gun weapon;

    private void Awake()
    {
        weapon = GetComponent<Gun>();
    }


    private void Update()
    {
        ammoCount.text = weapon.currentAmmo + "/" + weapon.allAmmo;
    }

}
