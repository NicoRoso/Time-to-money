using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip begginingSound;
    [SerializeField] private AudioSource m_Source;

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
        m_Source.PlayOneShot(begginingSound);
    }

    private void OnEnable()
    {
        AssaultPhase._assaultMusic += PlaySound;
        PreparingToAssault.preparedMusic += PlaySound;
    }

    private void OnDisable()
    {
        AssaultPhase._assaultMusic -= PlaySound;
        PreparingToAssault.preparedMusic -= PlaySound;
    }

    public void PlaySound(AudioClip clip)
    {
        m_Source.Stop();
        m_Source.PlayOneShot(clip);
    }
}
