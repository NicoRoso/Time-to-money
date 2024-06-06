using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Interactor : MonoBehaviour
{
    [SerializeField] private GameObject _keyPanel;

    [SerializeField] private AudioClip itemPick;

    public static Action<AudioClip> isPicked;

    private void OnDestroy()
    {
        isPicked?.Invoke(itemPick);
        _keyPanel.GetComponent<TimeInteractScript>().enabled = true;
    }
}
