using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float hitPoints;

    [SerializeField] private float movementSpeed;
    [SerializeField] private AttackTypes currentAttack = AttackTypes.BasicAttack;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate;
    private float _nextFire = 0f;

    [Header("Triple Shot PowerUp Parameters")]
    [SerializeField] private float tripleShotPowerUpDuration;

    [SerializeField] private GameObject tripleLaserPrefab;

    [Header("Speed PowerUp Parameters")]
    [SerializeField] private float speedPowerUpDuration;
    [SerializeField] private float speedPowerUpMultiplier;
    private bool _isSpeedPowerUpActive = false;

    [Header("Other")]
    private SpawnManager _spawnManager;

    [Header("Input Controls")]
    [SerializeField] private InputAction playerMovement;

    [SerializeField] private InputAction playerAttack;

    private void OnEnable()
    {
        playerMovement.Enable();
        playerAttack.Enable();
    } //New Input System Requirement
    private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
    } //New Input System Requirement

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }
    void Start()
    {
        transform.position = Vector3.down;
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
        if (!_isSpeedPowerUpActive)
            transform.Translate(direction * (movementSpeed * Time.deltaTime));
        else
            transform.Translate(direction * (movementSpeed * speedPowerUpMultiplier * Time.deltaTime));

        // Clamping position
        float clampedX = Mathf.Clamp(transform.position.x, -9.15f, 9.15f);
        float clampedY = Mathf.Clamp(transform.position.y, -3.9f, 5.9f);
        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
    private void Fire()
    {
        switch (currentAttack)
        {
            case AttackTypes.BasicAttack:
                Instantiate(laserPrefab,
                    new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z),
                    Quaternion.identity);
                break;
            case AttackTypes.TripleAttack:
                Instantiate(tripleLaserPrefab,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity);
                break;
        }
    }

    public void EnableTripleShotPowerUp()
    {
        currentAttack = AttackTypes.TripleAttack;
        StartCoroutine(DisableTripleShotRoutine());
    }
    private IEnumerator DisableTripleShotRoutine()
    {
        yield return new WaitForSeconds(tripleShotPowerUpDuration);
        currentAttack = AttackTypes.BasicAttack;
    }
    public void EnableSpeedPowerUp()
    {
        StartCoroutine(DisableSpeedRoutine());
        _isSpeedPowerUpActive = true;
    }
    private IEnumerator DisableSpeedRoutine()
    {
        yield return new WaitForSeconds(speedPowerUpDuration);
        _isSpeedPowerUpActive = false;
    }

    public void GetDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Debug.LogWarning("Game Over");

            Destroy(gameObject);
        }
    }
}

enum AttackTypes
{
    BasicAttack,
    TripleAttack
}