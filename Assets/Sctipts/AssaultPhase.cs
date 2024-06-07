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

    [SerializeField] private GameObject _assaultCanvas;

    private bool isAssault;

    private void Awake()
    {
        preparePhase = GetComponent<PreparingToAssault>();
    }

    private void OnEnable()
    {
        _assaultCanvas.SetActive(true);
        _assaultMusic?.Invoke(_clip);
        countdownTimer = assaultTime + CivilKillChecker.civilKillCount;
        isAssault = true;
    }

    private void OnDisable()
    {
        _assaultCanvas.SetActive(false);
        preparePhase.enabled = true;
        isAssault = false;
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

        if (isAssault)
        {

            PoliceAI[] policeAIs = FindObjectsOfType<PoliceAI>();

            foreach (PoliceAI policeAI in policeAIs)
            {
                policeAI.SetAssaultPhase();
            }
        }
        else
        {
            PoliceAI[] policeAIs = FindObjectsOfType<PoliceAI>();

            foreach (PoliceAI policeAI in policeAIs)
            {
                policeAI.SetPreparationPhase();
            }

        }
    }
}
