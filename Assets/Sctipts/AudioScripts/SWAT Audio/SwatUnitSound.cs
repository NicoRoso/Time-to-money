using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatUnitSound : MonoBehaviour
{

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _GunSource;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GetComponent<Hp>()._isDead += PlayMassiveAudio;
        GetComponent<PoliceShooting>()._isSpoted += PlayMassiveAudio;
        GetComponent<PoliceAI>().isOrdered += PlayMassiveAudio;
    }

    private void OnDisable()
    {
        GetComponent<Hp>()._isDead -= PlayMassiveAudio;
        GetComponent<PoliceShooting>()._isSpoted -= PlayMassiveAudio;
        GetComponent<PoliceAI>().isOrdered -= PlayMassiveAudio;
    }

    public void PlayGunSound(AudioClip clip)
    {
        _GunSource.PlayOneShot(clip);
    }

    public void PlayMassiveAudio(AudioClip[] clips)
    {
        AudioClip tempClip = clips[Random.Range(0, clips.Length)];
        _source.PlayOneShot(tempClip);
    }
}
