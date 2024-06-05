using System.Collections;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject _policeUnit;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int maxSpawnCount = 10;
    [SerializeField] private Transform spawnPoint;

    private int enemyCount;
    private bool _spawning;

    private void Start()
    {
        _spawning = true;
        StartCoroutine(SpawnPoliceUnits());
    }

    private IEnumerator SpawnPoliceUnits()
    {
        while (true)
        {
            if (_spawning)
            {
                if (enemyCount < maxSpawnCount)
                {
                    Instantiate(_policeUnit, spawnPoint.position, Quaternion.identity);
                    yield return new WaitForSeconds(_spawnDelay);
                }
                else
                {
                    _spawning = false;
                }
            }
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        enemyCount = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Hp hp = enemy.GetComponent<Hp>();
            if (hp != null && hp.enabled)
            {
                enemyCount++;
            }
        }

        if (!_spawning && enemyCount < maxSpawnCount)
        {
            _spawning = true;
        }
    }
}
