using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultPhase : MonoBehaviour
{
    public float assaultTime = 120f;
    private float countdownTimer;

    public bool endlessAssault;

    public PreparingToAssault preparePhase;

    [SerializeField] private AudioClip _clip;
    public static Action<AudioClip> _assaultMusic;

    private void Awake()
    {
        preparePhase = GetComponent<PreparingToAssault>();
    }

    private void OnEnable()
    {
        _assaultMusic?.Invoke(_clip);
        countdownTimer = assaultTime + CivilKillChecker.civilKillCount;
    }

    private void OnDisable()
    {
        preparePhase.enabled = true;
    }

    void Update()
    {
        if (!endlessAssault)
        {
            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0)
            {
                this.enabled = false;
            }
        }

        PoliceAI[] policeAIs = FindObjectsOfType<PoliceAI>();

        foreach (PoliceAI policeAI in policeAIs)
        {
            policeAI.SetAssaultPhase();
        }
    }
}
