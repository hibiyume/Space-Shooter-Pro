using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private InputAction playerMovement;
    [SerializeField] private InputAction playerAttack;

    [Header("Other")]
    [SerializeField] private GameObject laserPrefab;
    
    void Start()
    {
        transform.position = Vector3.zero;
    }
    
    void FixedUpdate()
    {
        MovePlayer();
        
        if (playerAttack.triggered)
            SpawnLaser();
    }

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

    private void MovePlayer()
    {
        // Moving player

        Vector3 direction = playerMovement.ReadValue<Vector2>();
        transform.Translate(direction * (speed * Time.deltaTime));

        // Clamping position
        float clampedX = Mathf.Clamp(transform.position.x, -9.15f, 9.15f);
        float clampedY = Mathf.Clamp(transform.position.y, -3.9f, 5.9f);
        transform.position = new Vector3(clampedX, clampedY, 0f);
    }

    private void SpawnLaser()
    {
        Instantiate(laserPrefab, transform.position, Quaternion.identity);
    }
}
