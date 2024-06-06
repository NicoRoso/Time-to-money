using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedAnim : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        TimeInteractScript.isStartOpen += Open;
    }

    private void OnDisable()
    {
        TimeInteractScript.isStartOpen -= Open;
    }

    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void PlaySound(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}
