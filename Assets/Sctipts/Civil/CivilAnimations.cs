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
    private string run = "isRunning";

    [SerializeField] AudioSource source;

    public bool isHostage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _hp = GetComponent<Hp>();
        isHostage = false;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_hp.isDead)
        {
            isHostage = false;
            HostageAnimation(isHostage);
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
        this.isHostage = isHostage;
        animator.SetBool(hostage, isHostage);
    }

    public void RunAnimation(bool isRunning)
    {
        animator.SetBool(run, isRunning);
    }

    public void Scream(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
