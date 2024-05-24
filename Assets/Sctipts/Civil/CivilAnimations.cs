using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] Hp _hp;
    private string dead = "Death";
    private string deadIndex = "DeathIndex";
    private string hostage = "isHostage";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _hp = GetComponent<Hp>();
    }

    private void Update()
    {
        if (_hp.isDead)
        {
            HostageAnimation(false);
            Death();
        }
    }

    public void Death()
    {
        animator.SetInteger(deadIndex, Random.Range(0, 5));
        animator.SetTrigger(dead);
    }

    public void HostageAnimation(bool isHostage)
    {
        animator.SetBool(hostage, isHostage);
    }
}
