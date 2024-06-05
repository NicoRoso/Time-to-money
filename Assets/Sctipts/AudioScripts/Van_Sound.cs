using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van_Sound : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void PlaySound(AudioClip clip)
    {
        source.Stop();
        source.PlayOneShot(clip);
    }
}
