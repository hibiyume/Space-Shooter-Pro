using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float hitPoints;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fireRate;
    private float _nextFire = 0f;
    
    [Header("Other")]
    [SerializeField] private GameObject projectilePrefab;
    private SpawnManager spawnManager;
    
    [Header("Input Controls")]
    [SerializeField] private InputAction playerMovement;
    [SerializeField] private InputAction playerAttack;

    private void OnEnable()
    {
        playerMovement.Enable();
        playerAttack.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
    }

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Start()
    {
        transform.position = Vector3.zero;
    }
    
    void Update()
    {
        Move();

        if (playerAttack.triggered && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            Fire();
        }
    }

    private void Move()
    {
        // Moving player

        Vector3 direction = playerMovement.ReadValue<Vector2>();
        transform.Translate(direction * (movementSpeed * Time.deltaTime));

        // Clamping position
        
        float clampedX = Mathf.Clamp(transform.position.x, -9.15f, 9.15f);
        float clampedY = Mathf.Clamp(transform.position.y, -3.9f, 5.9f);
        transform.position = new Vector3(clampedX, clampedY, 0f);
    }

    private void Fire()
    {
        Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z),
            Quaternion.identity);
    }

    public void DamagePlayer(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            spawnManager.OnPlayerDeath();
            Debug.LogWarning("Game Over");
            
            Destroy(gameObject);
        }
    }
}