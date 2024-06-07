using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePlayerDamage : MonoBehaviour
{
    public PlayerHp playerHp;
    private float timeSinceLastDamage;
    private const float armorRegenDelay = 3.5f;

    [SerializeField] private AudioClip[] takeDamageVoice;

    public static Action<AudioClip[]> isDamaged;

    private void Awake()
    {
        playerHp = GetComponent<PlayerHp>();
    }

    private void Update()
    {
        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= armorRegenDelay)
        {
            if (playerHp.armor < playerHp.maxArmor)
            {
                playerHp.armor += 10;
            }
            if (playerHp.armor > playerHp.maxArmor)
            {
                playerHp.armor = playerHp.maxArmor;
            }
            timeSinceLastDamage = 0f;
        }
    }

    public void DicreaseHp(int damage)
    {
        if (playerHp.hp <= playerHp.hp / 2)
        {
            ;
        }
        else if (playerHp.hp <= playerHp.hp / 3)
        {
            isDamaged?.Invoke(takeDamageVoice);
        }

        if (playerHp.armor > 0)
        {
            playerHp.armor -= damage/ 2;
            playerHp.hp -= 1;
        }
        else
        {
            playerHp.hp -= damage / 2;
        }

        timeSinceLastDamage = 0f;
    }
}
