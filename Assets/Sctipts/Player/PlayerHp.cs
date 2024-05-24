using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int maxHp;
    public int hp;
    public int maxArmor;
    public int armor;

    private void Awake()
    {
        maxArmor = 100;
        maxHp = 200;
        hp = maxHp;
        armor = maxArmor;
    }

    private void Update()
    {
        if (hp <= 0) 
        {
            Debug.Log("Died");
        }

        if (armor <= 0)
        {
            armor = 0;
        }
    }
}
