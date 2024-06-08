using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _Spawner;

    [SerializeField] private float _holdTime;

    private float holdTimer = 0f;

    private void Update()
    {
        if (!_Spawner.activeSelf)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= _holdTime)
            {
                _Spawner.SetActive(true);
            }
        }
    }
}
