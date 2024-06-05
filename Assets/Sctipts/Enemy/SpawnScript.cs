using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject _poilceUnit;

    [SerializeField] private float _spawnDelay;

    [SerializeField] private int maxSpawnCount;

    [SerializeField] private GameObject[] _allPoliceUnits;
}
