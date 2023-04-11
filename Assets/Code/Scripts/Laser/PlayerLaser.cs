using System;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [Header("Laser Parameters")]
    [SerializeField] private float speed = 8f;
    private const int Damage = 1;

    private void FixedUpdate()
    {
        Move();

        if (transform.position.y > 8f)
            DestroyLaser();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }
    private void DestroyLaser()
    {
        if (transform.parent)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
    }
}