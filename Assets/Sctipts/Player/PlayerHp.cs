using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] public int maxHp = 200;
    [SerializeField] public int hp;
    [SerializeField] public int maxArmor = 100;
    [SerializeField] public int armor;

    [SerializeField] public Image _hpBar;
    [SerializeField] public Image _armorBar;

    private void Awake()
    {
        hp = maxHp;
        armor = maxArmor;
        UpdateHpBar();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            SceneManager.LoadScene(2);
        }

        if (armor < 0)
        {
            armor = 0;
        }

        UpdateHpBar();
        UpdateArmorBar();
    }

    public void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            int damageToArmor = Mathf.Min(damage, armor);
            armor -= damageToArmor;
            int damageToHp = damage - damageToArmor;
            hp -= damageToHp;
        }
        else
        {
            hp -= damage;
        }

        if (hp < 0) hp = 0;
        UpdateHpBar();
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > maxHp) hp = maxHp;
        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        if (_hpBar != null)
        {
            _hpBar.fillAmount = (float)hp / maxHp;
        }
    }

    private void UpdateArmorBar()
    {
        if (_armorBar != null)
        {
            _armorBar.fillAmount = (float)armor / maxArmor;
        }
    }
}
