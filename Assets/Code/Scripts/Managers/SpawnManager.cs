using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [Header("Other")]
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(delayBeforeStartSpawning);
        while (_gameManager.ArePlayersAlive)
        {
            yield return new WaitForSeconds(Random.Range(enemyMinSpawnRate, enemyMaxSpawnRate));
            
            float x = Random.Range(-9f, 9f);
            float y = enemyPrefab.transform.position.y;
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyFolder);

            if (_gameManager.ArePlayersAlive)
                StopSpawning();
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(delayBeforeStartSpawning);
        while (_gameManager.ArePlayersAlive)
        {
            yield return new WaitForSeconds(Random.Range(powerUpMinSpawnRate, powerUpMaxSpawnRate));
            
            GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

            float x = Random.Range(-9f, 9f);
            float y = powerUpPrefab.transform.position.y;
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(powerUpPrefab, spawnPos, Quaternion.identity, powerUpFolder);

            if (!_gameManager.ArePlayersAlive)
                StopSpawning();
        }
    }
    
    private void StopSpawning()
    {
        StopCoroutine(SpawnEnemyRoutine());
        StopCoroutine(SpawnPowerUpRoutine());
    }
}