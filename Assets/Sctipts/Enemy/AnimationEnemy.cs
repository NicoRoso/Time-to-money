using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnemy : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Hp _hp;
    private string dead = "Death";
    private string deadIndex = "DeathIndex";
    private string hit = "GetHit";
    private string walk = "Walking";
    private string fire = "Fired";

    void Awake()
    {
        animator = GetComponent<Animator>();
        _hp = GetComponent<Hp>();

        TakeDamage takeDamage = GetComponentInChildren<TakeDamage>();
        if (takeDamage != null && !_hp.isDead)
        {
            takeDamage.SetOnTakeDamage(TakeHit);
        }
    }

    private void Update()
    {
        if (_hp.isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        animator.SetInteger(deadIndex, Random.Range(0, 5));
        animator.SetTrigger(dead);
        this.enabled = false;
    }

    public void TakeHit()
    {
        animator.SetTrigger(hit);
    }

    public void Walking(bool status)
    {
        animator.SetBool(walk, status);
    }

    public void Crouch()
    {

    }

    public void Fire()
    {
        animator.SetTrigger(fire);
    }
}
