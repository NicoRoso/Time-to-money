using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePlayerDamage : MonoBehaviour
{
    public PlayerHp playerHp;
    private float timeSinceLastDamage;
    private const float armorRegenDelay = 3.5f;
    private const int armorRegenRate = 10;

    [SerializeField] private AudioClip[] takeDamageVoice;

    public static System.Action<AudioClip[]> isDamaged;

    private void Awake()
    {
        playerHp = GetComponent<PlayerHp>();
    }

    private void Update()
    {
        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= armorRegenDelay)
        {
            RegenerateArmor();
        }
    }

    private void RegenerateArmor()
    {
        if (playerHp.armor < playerHp.maxArmor)
        {
            playerHp.armor += armorRegenRate;
            if (playerHp.armor > playerHp.maxArmor)
            {
                playerHp.armor = playerHp.maxArmor;
            }
        }
        timeSinceLastDamage = 0f;
    }

    public void DecreaseHp(int damage)
    {
        playerHp.TakeDamage(damage);

        Color hpBarColor;
        if (playerHp.hp <= playerHp.maxHp / 3)
        {
            hpBarColor = new Color(1f, 0f, 0f, 0.5f);
            isDamaged?.Invoke(takeDamageVoice);
        }
        else if (playerHp.hp <= playerHp.maxHp / 2)
        {
            hpBarColor = new Color(1f, 1f, 0f, 0.5f);
        }
        else
        {
            hpBarColor = new Color(0f, 1f, 0f, 0.5f);
        }

        playerHp._hpBar.color = hpBarColor;
        timeSinceLastDamage = 0f;
    }
}
