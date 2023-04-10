using System;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Asteroid Parameters")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject explosionPrefab;

    [Header("Other")]
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") || col.tag.Equals("PlayerLaser"))
        {
            _spawnManager.StartSpawning();

            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject.GetComponent<SpriteRenderer>(), 0.33f);
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 2.5f);
            
            if (col.tag.Equals("Player"))
                col.GetComponent<Player>().GetDamage(2);
            else if (col.tag.Equals("PlayerLaser"))
                Destroy(col.gameObject);
            
            Destroy(gameObject, 2.5f);
        }
    }
}
