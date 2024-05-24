using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparingToAssault : MonoBehaviour
{
    public float preparationTime = 60f;
    private float countdownTimer;

    public AssaultPhase assaultPhase;

    private void Awake()
    {
        assaultPhase = GetComponent<AssaultPhase>();
    }

    private void OnEnable()
    {
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
            Debug.Log("Assault prepared!");
            this.enabled = false;
        }
    }
}
