using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceHP : MonoBehaviour
{
    [SerializeField] private int maxHp;
    public int currentHp;
    [SerializeField] private float timeToDestroy;
    public bool isDead = false;

    public event System.Action OnDeath;

    public AudioClip[] _deathLines;
    public Action<AudioClip[]> _isDead;

    [Header("Drop Items")]
    [SerializeField] private GameObject firstKit;
    [SerializeField] private GameObject ammoBox;
    [SerializeField] private float dropChance = 0.5f;

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
        isDead = true;
        OnDeath?.Invoke();
        _isDead?.Invoke(_deathLines);
        GetComponent<NavMeshAgent>().speed = 0;
        HandleItemDrop();

        Destroy(this.gameObject, timeToDestroy);
        this.enabled = false;
    }

    private void HandleItemDrop()
    {
        float randomValue = UnityEngine.Random.value;

        if (randomValue <= dropChance)
        {
            GameObject itemToDrop = UnityEngine.Random.value > 0.5f ? firstKit : ammoBox;

            if (itemToDrop != null)
            {
                Instantiate(itemToDrop, transform.position, Quaternion.identity);
            }
        }
    }
}
