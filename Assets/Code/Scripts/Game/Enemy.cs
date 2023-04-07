using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private int damagePoints;
    [SerializeField] private int scoreWhenDestroyed;

    [Header("Animation Parameters")]
    private Animator _animator;

    [Header("Other")]
    private SpawnManager _spawnManager;
    private Player _player;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _player = FindObjectOfType<Player>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        MoveDown();

        if (transform.position.y < -6f)
        {
            if (_spawnManager.IsPlayerAlive)
                RespawnAtTop();
            else
                Destroy(gameObject);
        }
    }

    private void MoveDown()
    {
        // Moving enemy
        Vector3 direction = Vector3.down;
        transform.Translate(direction * (movementSpeed * Time.deltaTime));
    }
    private void RespawnAtTop()
    {
        float x = Random.Range(-9f, 9f);
        float y = 8f;
        float z = transform.position.z;
        Vector3 newPos = new Vector3(x, y, z);

        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Player":
                OnTriggerWithPlayer(col);
                break;
            case "Laser":
                OnTriggerWithLaser(col);
                break;
        }
    }
    private void OnTriggerWithLaser(Collider2D col)
    {
        _player.AddScore(scoreWhenDestroyed);
        _animator.SetTrigger("OnEnemyDeath");
        movementSpeed *= 0.33f;

        Destroy(col.gameObject);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject, 1.3f);
    }
    private void OnTriggerWithPlayer(Collider2D col)
    {
        _player.AddScore(scoreWhenDestroyed);
        _animator.SetTrigger("OnEnemyDeath");
        movementSpeed *= 0.33f;
        
        col.GetComponent<Player>().GetDamage(damagePoints);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject, 1.3f);
    }
}