using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField] private TMP_Text values;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        values.text = "Money: " + SetValue.AmountValue+"$";
    }
}
