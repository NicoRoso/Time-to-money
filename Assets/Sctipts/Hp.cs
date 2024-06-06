using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hp : MonoBehaviour
{
    [SerializeField] private int maxHp;
    public int currentHp;
    [SerializeField] private float timeToDestroy;
    public bool isDead = false;

    public event System.Action OnDeath;

    public AudioClip[] _deathLines;
    public Action<AudioClip[]> _isDead;

    private void Awake()
    {
        currentHp = maxHp;
        if (_deathLines == null)
        {
            return;
        }
    }

    private void Update()
    {
        if (currentHp <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<NavMeshAgent>().speed = 0;
        isDead = true;
        OnDeath?.Invoke();
        _isDead?.Invoke(_deathLines);
        Destroy(this.gameObject, timeToDestroy);
        this.enabled = false;
    }
}
