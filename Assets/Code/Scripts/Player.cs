using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private int hitPoints;
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate;
    private float _nextFire = 0f;
    private enum AttackTypes
    {
        BasicAttack,
        TripleAttack
    }
    [SerializeField] private AttackTypes currentAttack = AttackTypes.BasicAttack;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject[] playerEngineReferences;
    
    [Header("Triple Shot PowerUp Parameters")]
    [SerializeField] private float tripleShotPowerUpDuration;
    [SerializeField] private GameObject tripleLaserPrefab;

    [Header("Speed PowerUp Parameters")]
    [SerializeField] private float speedPowerUpDuration;
    [SerializeField] private float speedPowerUpMultiplier;

    [Header("Shield PowerUp Parameters")]
    [SerializeField] private GameObject shieldPowerUpReference;
    private bool _hasShield = false;

    [Header("Other")]
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private GameManager _gameManager;
    
    [Header("Audio")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip laserAudioClip;
    
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
        _spawnManager = FindObjectOfType<SpawnManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Move();

        TryToFire();
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
    private void TryToFire()
    {
        if (playerAttack.triggered && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            Fire();
        }
    }
    private void Fire()
    {
        switch (currentAttack)
        {
            case AttackTypes.BasicAttack:
                Instantiate(laserPrefab,
                    new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z),
                    Quaternion.identity);
                _audioSource.pitch = 1f;
                break;
            case AttackTypes.TripleAttack:
                Instantiate(tripleLaserPrefab,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity);
                _audioSource.pitch = 0.66f;
                break;
        }
        _audioSource.clip = laserAudioClip;
        _audioSource.Play();
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
        movementSpeed *= speedPowerUpMultiplier;
        StartCoroutine(DisableSpeedRoutine());
    }
    private IEnumerator DisableSpeedRoutine()
    {
        yield return new WaitForSeconds(speedPowerUpDuration);
        movementSpeed /= speedPowerUpMultiplier;
    }
    public void EnableShieldPowerUp()
    {
        _hasShield = true;
        shieldPowerUpReference.SetActive(true);
        StartCoroutine(DisableTripleShotRoutine());
    }

    public void GetDamage(int damage)
    {
        if (!_hasShield)
        {
            hitPoints -= damage;
            _uiManager.UpdateLivesImage(hitPoints);
        }
        else
        {
            shieldPowerUpReference.SetActive(false);
            _hasShield = false;
        }
        
        switch (hitPoints)
        {
            case 2:
                playerEngineReferences[Random.Range(0, 2)].SetActive(true);
                break;
            case 1:
                playerEngineReferences[0].SetActive(true);
                playerEngineReferences[1].SetActive(true);
                break;
            case 0:
                _gameManager.OnGameOver();

                DestroyPlayer();
                break;
        }
    }
    private void DestroyPlayer()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.25f);
        Destroy(this); //script
    }
}