using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class TimeInteractScript : MonoBehaviour
{
    public static Action isStartOpen;
    public static Action<AudioClip> isStartOpenSound;

    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private float countdownDuration;
    [SerializeField] private TMP_Text textTimer;

    private bool isOpened;
    private float timeRemaining;

    private bool isActivated;

    private void Awake()
    {
        this.enabled = false;
        isOpened = false;
        timeRemaining = countdownDuration;
        isActivated = false;
    }

    public void StartCountdown()
    {
        if (!isActivated)
        {
            timeRemaining = countdownDuration;
            StartCoroutine(CountdownCoroutine());
            isStartOpenSound?.Invoke(clip2);
            isActivated = true;
        }
    }

    IEnumerator CountdownCoroutine()
    {
        while (timeRemaining > 0)
        {
            UpdateTimerDisplay(timeRemaining);
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        UpdateTimerDisplay(0);

        isStartOpen?.Invoke();

        if (!isOpened)
        {
            isStartOpenSound?.Invoke(clip);
            isOpened = true;
        }
    }

    private void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        textTimer.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
