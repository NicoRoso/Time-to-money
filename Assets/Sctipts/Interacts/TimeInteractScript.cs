using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInteractScript : MonoBehaviour
{
    public static Action isStartOpen;

    [SerializeField] float countdownDuration = 5f;

    private void Awake()
    {
        this.enabled = false;
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(countdownDuration);

        isStartOpen?.Invoke();
    }
}
