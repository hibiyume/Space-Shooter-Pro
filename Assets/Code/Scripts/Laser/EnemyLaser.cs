using System;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [Header("Laser Parameters")]
    [SerializeField] private float speed = 8f;
    private const int Damage = 1;

    private void FixedUpdate()
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
        if (other.tag.Equals("Player1") || other.tag.Equals("Player2"))
        {
            other.GetComponent<Player>().GetDamage(Damage);
            Destroy(gameObject);
        }
    }
}