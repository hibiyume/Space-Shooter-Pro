using System;
using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Power Up Parameters")]
    [SerializeField] private PowerUpTypes powerUpType;
    [SerializeField] private float movementSpeed;
    private enum PowerUpTypes
    {
        TripleShot,
        Speed,
        Shield
    }
    
    [Header("Audio")]
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Move();

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (movementSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player1") || col.tag.Equals("Player2"))
        {
            switch (powerUpType)
            {
                case PowerUpTypes.TripleShot:
                    col.GetComponent<Player>().EnableTripleShotPowerUp();
                    break;
                case PowerUpTypes.Speed:
                    col.GetComponent<Player>().EnableSpeedPowerUp();
                    break;
                case PowerUpTypes.Shield:
                    col.GetComponent<Player>().EnableShieldPowerUp();
                    break;
            }

            _audioSource.Play();
            
            DestroyComponents();
            StartCoroutine(DestroyPowerUp());
        }
    }

    private void DestroyComponents()
    {
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());
    }
    private IEnumerator DestroyPowerUp()
    {
        yield return new WaitUntil(() => !_audioSource.isPlaying);
        Destroy(gameObject);
    }
}