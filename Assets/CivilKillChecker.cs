using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilKillChecker : MonoBehaviour
{
    public static int civilKillCount;
    public int killCount;
    private void Awake()
    {
        civilKillCount = 0;
        GameObject[] civils = GameObject.FindGameObjectsWithTag("Civil");
        foreach (GameObject civil in civils)
        {
            civil.GetComponent<Hp>().OnDeath += IncreaseKillCount;
        }
    }

    private void FixedUpdate()
    {
        killCount = civilKillCount;
    }

    public void IncreaseKillCount()
    {
        civilKillCount++;
    }

}
