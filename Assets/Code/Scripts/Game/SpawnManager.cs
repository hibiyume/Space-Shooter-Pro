using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float delayBeforeStartSpawning;

    [Header("Folders")]
    [SerializeField] private Transform enemyFolder;
    [SerializeField] private Transform powerUpFolder;

    [Header("Enemies")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemyMinSpawnRate;
    [SerializeField] private float enemyMaxSpawnRate;

    [Header("PowerUps")]
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float powerUpMinSpawnRate;
    [SerializeField] private float powerUpMaxSpawnRate;

    public bool IsPlayerAlive { get; private set; } = true;
    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(delayBeforeStartSpawning);
        while (IsPlayerAlive)
        {
            float x = Random.Range(-9f, 9f);
            float y = enemyPrefab.transform.position.y;
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyFolder);

            yield return new WaitForSeconds(Random.Range(enemyMinSpawnRate, enemyMaxSpawnRate));
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(delayBeforeStartSpawning);
        while (IsPlayerAlive)
        {
            GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

            float x = Random.Range(-9f, 9f);
            float y = powerUpPrefab.transform.position.y;
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(powerUpPrefab, spawnPos, Quaternion.identity, powerUpFolder);

            yield return new WaitForSeconds(Random.Range(powerUpMinSpawnRate, powerUpMaxSpawnRate));
        }
    }

    public void OnPlayerDeath()
    {
        IsPlayerAlive = false;
    }
}