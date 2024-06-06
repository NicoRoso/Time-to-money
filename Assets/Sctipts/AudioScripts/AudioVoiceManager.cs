using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVoiceManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        CivilIntoRobber.isBegun += PlayVoiceLine;
        PlayerToCivilGround.isSaid += PlayVoiceLine;
        TakePlayerDamage.isDamaged += PlayVoiceLine;
        PreparingToAssault._anticipated += PlayVoiceLine;
        Card_Interactor.isPickedVoiceLine += PlayOneVoice;
    }

    private void OnDisable()
    {
        CivilIntoRobber.isBegun -= PlayVoiceLine;
        PlayerToCivilGround.isSaid -= PlayVoiceLine;
        TakePlayerDamage.isDamaged -= PlayVoiceLine;
        PreparingToAssault._anticipated -= PlayVoiceLine;
        Card_Interactor.isPickedVoiceLine -= PlayOneVoice;
    }

    public void PlayVoiceLine(AudioClip[] clips)
    {
        if (!source.isPlaying)
        {
            AudioClip tmp = clips[UnityEngine.Random.Range(0, clips.Length)];
            source.PlayOneShot(tmp);
        }
    }

    public void PlayOneVoice(AudioClip clip)
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(clip);
        }
    }
}
