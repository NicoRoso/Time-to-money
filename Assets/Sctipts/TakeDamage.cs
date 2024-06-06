using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private Action onTakedDamage;

    [SerializeField] private AudioClip[] takeHitLines;

    [SerializeField] private AudioSource source;

    [SerializeField] private PoliceHP hp;

    private void Awake()
    {
        hp = GetComponentInParent<PoliceHP>();
        source = GetComponentInParent<AudioSource>();
    }

    public void DecreaseHP(int amount)
    {
        if (hp != null && hp.currentHp > 0)
        {
            hp.currentHp -= amount;
            if (takeHitLines != null && takeHitLines.Length > 0)
            {
                AudioClip tmpClip = takeHitLines[UnityEngine.Random.Range(0, takeHitLines.Length)];
                source.PlayOneShot(tmpClip);
            }
            onTakedDamage?.Invoke();
        }
        else
        {
            this.enabled = false;
        }
    }

    public void SetOnTakeDamage(Action callback)
    {
        onTakedDamage = callback;
    }
}
