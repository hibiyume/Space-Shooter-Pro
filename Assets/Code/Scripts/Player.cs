using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float speed;
    
    void Start()
    {
        transform.position = Vector3.zero;
    }
    
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Moving player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f) * (speed * Time.deltaTime);
        transform.Translate(direction);

        // Clamping position
        float clampedX = Mathf.Clamp(transform.position.x, -9.15f, 9.15f);
        float clampedY = Mathf.Clamp(transform.position.y, -3.9f, 5.9f);
        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
}
