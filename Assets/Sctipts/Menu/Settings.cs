using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider masterVol, sfxVol, musicVol, voiceVol;

    [SerializeField] AudioMixer mainAudioMixer;

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("Master", masterVol.value);
    }
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFX", sfxVol.value);
    }
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("Music", musicVol.value);
    }
    public void ChangeVoiceVolume()
    {
        mainAudioMixer.SetFloat("Voice", voiceVol.value);
    }
}
