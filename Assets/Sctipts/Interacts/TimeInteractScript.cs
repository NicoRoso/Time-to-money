using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInteractScript : MonoBehaviour
{
    public static Action isStartOpen;
    public static Action<AudioClip> isStartOpenSound;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;

    [SerializeField] float countdownDuration = 5f;

    private bool isOpened;

    private void Awake()
    {
        this.enabled = false;
        isOpened = false;
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
        isStartOpenSound?.Invoke(clip2);
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(countdownDuration);

        isStartOpen?.Invoke();

        if (!isOpened)
        {
            isStartOpenSound?.Invoke(clip);
            isOpened = true;
        }
    }
}
