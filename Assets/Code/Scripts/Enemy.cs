using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float minFireRate;
    [SerializeField] private float maxFireRate;
    private float _canFire = 0f;
    [SerializeField] private int scoreWhenDestroyed;
    [SerializeField] private GameObject enemyLaserPrefab;
    private bool _isDestroyed;
    private const int Damage = 1;

    [Header("Audio")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip explosionSoundClip;
    [SerializeField] private AudioClip laserSoundClip;

    [Header("Other")]
    private GameManager _gameManager;
    private Animator _animator;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        MoveDown();
        RespawnAtTopIfOutOfBounds();

        if (Time.time > _canFire && !_isDestroyed && _gameManager.ArePlayersAlive)
        {
            Fire();
            _canFire = Time.time + Random.Range(minFireRate, maxFireRate);
        }
    }

    private void MoveDown()
    {
        Vector3 direction = Vector3.down;
        transform.Translate(direction * (movementSpeed * Time.deltaTime));
    }
    private void RespawnAtTopIfOutOfBounds()
    {
        if (transform.position.y < -6f)
        {
            if (_gameManager.ArePlayersAlive)
            {
                Vector3 newPos = new Vector3(Random.Range(-9f, 9f), 8f, transform.position.z);
                transform.position = newPos;
            }
            else
                Destroy(gameObject);
        }
    }
    private void Fire()
    {
        Instantiate(enemyLaserPrefab,
            new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z),
            Quaternion.identity);

        _audioSource.clip = laserSoundClip;
        _audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player1") || col.tag.Equals("Player2") || col.tag.Equals("PlayerLaser"))
            OnTriggerWithEnemy(col);
    }
    private void OnTriggerWithEnemy(Collider2D col)
    {
        _isDestroyed = true;
        movementSpeed *= 0.33f;
        _gameManager.AddScore(scoreWhenDestroyed);

        _animator.SetTrigger("OnEnemyDeath");
        _audioSource.clip = explosionSoundClip;
        _audioSource.Play();

        if (col.tag.Equals("Player1") || col.tag.Equals("Player2"))
            col.GetComponent<Player>().GetDamage(Damage);
        else if (col.tag.Equals("PlayerLaser"))
            Destroy(col.gameObject);

        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject, 2f);
    }
}