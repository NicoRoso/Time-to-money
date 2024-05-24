using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private Action onTakedDamage;

    [SerializeField] private Hp hp;

    private void Awake()
    {
        hp = GetComponentInParent<Hp>();
    }

    public void DecreaseHP(int amount)
    {
        hp.currentHp -= amount;
        Debug.Log(hp.gameObject.name + " потерял " + amount + " HP. Текущее HP: " + hp.currentHp);

        onTakedDamage?.Invoke();
    }

    public void SetOnTakeDamage(Action callback)
    {
        onTakedDamage = callback;
    }
}
