using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparingToAssault : MonoBehaviour
{
    public float preparationTime = 60f;
    private float antcipationPart;
    private float countdownTimer;

    public AssaultPhase assaultPhase;

    private bool anticipating;

    [SerializeField] private AudioClip controlClip;
    [SerializeField] private AudioClip anticipationClip;
    [SerializeField] private AudioClip[] anticipationVoiceLines;
    public static Action<AudioClip> preparedMusic;
    public static Action<AudioClip[]> _anticipated;

    private void Awake()
    {
        antcipationPart = preparationTime/2;
        assaultPhase = GetComponent<AssaultPhase>();
    }

    private void OnEnable()
    {
        preparedMusic?.Invoke(controlClip);
        anticipating = false;
        countdownTimer = preparationTime - CivilKillChecker.civilKillCount;
        PoliceAI[] policeAIs = FindObjectsOfType<PoliceAI>();

        foreach (PoliceAI policeAI in policeAIs)
        {
            policeAI.SetPreparationPhase();
        }
    }

    private void OnDisable()
    {
        assaultPhase.enabled = true;
    }

    void Update()
    {
        countdownTimer -= Time.deltaTime;

        if (countdownTimer <= 0)
        {
            this.enabled = false;
        }

        if (countdownTimer <= (antcipationPart) && !anticipating)
        {
            anticipating = true;
            preparedMusic?.Invoke(anticipationClip);
            _anticipated?.Invoke(anticipationVoiceLines);
        }
    }
}
