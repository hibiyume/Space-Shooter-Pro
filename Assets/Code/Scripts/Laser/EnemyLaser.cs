using System;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [Header("Laser Parameters")]
    [SerializeField] private float speed = 8f;
    private const int Damage = 1;

    [Header("Other")]
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        Move();

        if (transform.position.y < -6f)
            DestroyLaser();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
    }
    private void DestroyLaser()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            _player.GetDamage(Damage);
            Destroy(gameObject);
        }
    }
}