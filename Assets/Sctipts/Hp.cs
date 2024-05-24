using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField] private int maxHp;
    public int currentHp;
    [SerializeField] private float timeToDestroy;
    public bool isDead = false;

    public event System.Action OnDeath;

    private void Awake()
    {
        currentHp = maxHp;
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
        Destroy(this.gameObject, timeToDestroy);
    }
}
