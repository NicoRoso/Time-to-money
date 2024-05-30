using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Interactor : MonoBehaviour
{
    [SerializeField] private GameObject _keyPanel;

    private void OnDestroy()
    {
        _keyPanel.GetComponent<TimeInteractScript>().enabled = true;
    }
}
