using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Interactor : MonoBehaviour
{
    [SerializeField] private GameObject _keyPanel;

    [SerializeField] private AudioClip itemPick;
    [SerializeField] private AudioClip itemPickVoice;

    public static Action<AudioClip> isPicked;
    public static Action<AudioClip> isPickedVoiceLine;

    private void OnDestroy()
    {
        isPicked?.Invoke(itemPick);
        isPickedVoiceLine?.Invoke(itemPickVoice);
        _keyPanel.GetComponent<TimeInteractScript>().enabled = true;
    }
}
