using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject settings;

    [SerializeField] AudioClip pressButton;

    public static Action<AudioClip> isButtonPressed;

    private void Start()
    {
        menu.SetActive(true);
        settings.SetActive(false);
        equipment.SetActive(false);
    }

    public void StartNewGame()
    {
        SetValue.AmountValue = 0;
        equipment.SetActive(true);
        menu.SetActive(false);
        isButtonPressed?.Invoke(pressButton);
    }

    public void Settings()
    {
        settings.SetActive(true);
        menu.SetActive(false);
        isButtonPressed?.Invoke(pressButton);
    }

    public void Back()
    {
        menu.SetActive(true);
        settings.SetActive(false);
        equipment.SetActive(false);
        isButtonPressed?.Invoke(pressButton);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
