using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float hitPoints;
    [SerializeField] private float movementSpeed;
    [SerializeField] private AttackType currentAttack = AttackType.BasicAttack;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate;
    private float _nextFire = 0f;
    
    [Header("Triple Shot Parameters")]
    [SerializeField] private float tripleShotDuration;
    [SerializeField] private GameObject tripleLaserPrefab;
    
    [Header("Other")]
    private SpawnManager spawnManager;

    [Header("Input Controls")]
    [SerializeField] private InputAction playerMovement;

    [SerializeField] private InputAction playerAttack;

    private void OnEnable()
    {
        playerMovement.Enable();
        playerAttack.Enable();
    }   //New Input System Requirement
    private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
    }  //New Input System Requirement

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
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
        transform.Translate(direction * (movementSpeed * Time.deltaTime));

        // Clamping position

        float clampedX = Mathf.Clamp(transform.position.x, -9.15f, 9.15f);
        float clampedY = Mathf.Clamp(transform.position.y, -3.9f, 5.9f);
        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
    private void Fire()
    {
        switch (currentAttack)
        {
            case AttackType.BasicAttack:
                Instantiate(laserPrefab,
                    new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z),
                    Quaternion.identity);
                break;
            case AttackType.TripleAttack:
                Instantiate(tripleLaserPrefab,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity);
                break;
        }
    }

    public void EnableTripleShot()
    {
        currentAttack = AttackType.TripleAttack;
        StartCoroutine(DisableTripleShotRoutine());
    }
    private IEnumerator DisableTripleShotRoutine()
    {
        yield return new WaitForSeconds(tripleShotDuration);
        currentAttack = AttackType.BasicAttack;
    }
    
    public void GetDamage(float damage)
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

enum AttackType
{
    BasicAttack,
    TripleAttack,
}