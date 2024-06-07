using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Kit : MonoBehaviour
{
    [SerializeField] PlayerHp hp;

    [SerializeField] private int amount;

    private void Start()
    {
        hp = FindAnyObjectByType(typeof(PlayerHp)) as PlayerHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHp>())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        hp.Heal(amount);
    }
}
