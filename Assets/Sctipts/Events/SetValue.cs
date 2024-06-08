using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValue : MonoBehaviour
{
    public static int AmountValue;

    [SerializeField] private int Value;

    public static Action<int> isDestroyd;

    [SerializeField] private GameObject endMission;


    private void OnDestroy()
    {
        if (endMission != null)
        {
            endMission.SetActive(true);
        }

        AmountValue += Value;
        isDestroyd?.Invoke(Value);
    }
}
