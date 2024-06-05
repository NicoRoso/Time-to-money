using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatUnitSound : MonoBehaviour
{

    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Hp._isDead += PlayMassiveAudio;
        PoliceShooting._isSpoted += PlayMassiveAudio;
        PoliceAI.isOrdered += PlayMassiveAudio;
    }

    private void OnDisable()
    {
        Hp._isDead -= PlayMassiveAudio;
        PoliceShooting._isSpoted -= PlayMassiveAudio;
        PoliceAI.isOrdered -= PlayMassiveAudio;
    }

    public void PlayGunSound(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }

    public void PlayMassiveAudio(AudioClip[] clips)
    {
        AudioClip tempClip = clips[Random.Range(0, clips.Length)];
        _source.PlayOneShot(tempClip);
    }
}
