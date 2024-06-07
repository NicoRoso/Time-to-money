using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowValue : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float displayDuration = 3f;

    private Coroutine displayCoroutine;

    private void OnEnable()
    {
        SetValue.isDestroyd += CalculateValue;
    }

    private void OnDisable()
    {
        SetValue.isDestroyd -= CalculateValue;
    }

    public void CalculateValue(int value)
    {
        panel.SetActive(true);
        text.text = "Money: " + value+"$";

        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        displayCoroutine = StartCoroutine(HidePanelAfterDelay());
    }

    private IEnumerator HidePanelAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        panel.SetActive(false);
    }
}
