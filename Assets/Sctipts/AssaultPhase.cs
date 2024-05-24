using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultPhase : MonoBehaviour
{
    public float assaultTime = 120f;
    private float countdownTimer;

    public bool endlessAssault;

    public PreparingToAssault preparePhase;

    private void Awake()
    {
        preparePhase = GetComponent<PreparingToAssault>();
    }

    private void OnEnable()
    {
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
                Debug.Log("Preparation Phase Now!");
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
