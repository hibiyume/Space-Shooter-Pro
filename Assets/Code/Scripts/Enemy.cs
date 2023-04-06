using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damagePoints;

    [Header("Other")]
    [SerializeField] private SpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    private void Update()
    {
        MoveDown();

        if (transform.position.y < -6f)
        {
            if (spawnManager.IsPlayerAlive)
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
        if (col.tag.Equals("Laser"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnTriggerWithPlayer(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            col.GetComponent<Player>().GetDamage(damagePoints);
            Destroy(gameObject);
        }
    }
}