using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerController player;

    private string reloadName = "Reload";
    private string walkingName = "isWalking";
    private string afterShotName = "AfterShot";
    private string aimName = "isAiming";
    private string hideName = "Hide";
    private string fireName = "Fire";


    public static Action<AudioClip> isSounded;

    [SerializeField] private GunAim gunAnim;
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        gunAnim = GetComponent<GunAim>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Gun.gunFired += FireAnim;
    }

    private void OnDisable()
    {
        Gun.gunFired -= FireAnim;
    }


    private void Update()
    {
        if (player.isMoving)
        {
            WalkingAnimation(true);
        }
        else
        {
            WalkingAnimation(false);
        }

        AimAnimation();
    }
    public void ReloadingAnimation()
    {
        animator.SetTrigger(reloadName);
    }
    public void HideAnimation()
    {
        animator.SetTrigger(hideName);
    }

    public void FireAnim()
    {
        animator.SetTrigger(fireName);
    }

    public void WalkingAnimation(bool isWalking)
    {
        animator.SetBool(walkingName, isWalking);
    }
    public void AimAnimation()
    {
        animator.SetBool(aimName, gunAnim.isAiming);
    }

    public void ShotgunAfterFire()
    {
        animator.SetTrigger(afterShotName);
    }


    public void Sound(AudioClip clip)
    {
        isSounded?.Invoke(clip);
    }
}
