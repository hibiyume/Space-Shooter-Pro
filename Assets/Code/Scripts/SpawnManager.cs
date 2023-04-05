using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform enemyFolder;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate;

    public bool IsPlayerAlive { get; set; } = true;
    
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (IsPlayerAlive)
        {
            float x = Random.Range(-9f, 9f);
            float y = enemyPrefab.transform.position.y;
            float z = enemyPrefab.transform.position.z;
            Vector3 spawnPos = new Vector3(x, y, z);

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyFolder);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void OnPlayerDeath()
    {
        IsPlayerAlive = false;
    }
}
