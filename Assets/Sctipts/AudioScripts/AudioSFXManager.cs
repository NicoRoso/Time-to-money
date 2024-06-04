using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSFXManager : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GunAnimationController.isSounded += PlaySound;
        Gun.emptyFired += PlaySound;
    }

    private void OnDisable()
    {
        GunAnimationController.isSounded -= PlaySound;
        Gun.emptyFired -= PlaySound;
    }


    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}