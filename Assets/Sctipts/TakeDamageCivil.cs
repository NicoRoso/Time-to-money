using System;
using UnityEngine;

public class TakeDamageCivil : MonoBehaviour
{
    private Action onCivilTakedDamage;

    [SerializeField] private AudioClip[] takeHitLines;

    [SerializeField] private AudioSource source;

    [SerializeField] private Hp hp;

    private void Awake()
    {
        hp = GetComponentInParent<Hp>();
        source = GetComponentInParent<AudioSource>();
    }

    public void DecreaseCivilHP(int amount)
    {
        if (hp.currentHp > 0)
        {
            hp.currentHp -= amount;
            if (takeHitLines != null && takeHitLines.Length > 0)
            {
                AudioClip tmpClip = takeHitLines[UnityEngine.Random.Range(0, takeHitLines.Length)];
                source.PlayOneShot(tmpClip);
            }
            onCivilTakedDamage?.Invoke();
        }
        else
        {
            this.enabled = false;
        }
    }

    public void SetOnTakeDamage(Action callback)
    {
        onCivilTakedDamage = callback;
    }
}
