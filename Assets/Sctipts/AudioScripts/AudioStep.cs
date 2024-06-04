using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioStep : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerController.isWalking += PlayStepSound;
        PlayerController.isRunning += PlayStepSound;
    }

    private void OnDisable()
    {
        PlayerController.isWalking -= PlayStepSound;
        PlayerController.isRunning -= PlayStepSound;
    }

    public void PlayStepSound(AudioClip clip)
    {
        if (!source.isPlaying)
            source.PlayOneShot(clip);
    }
}
