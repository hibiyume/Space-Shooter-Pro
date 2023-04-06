using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    
    private void Update()
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
        if (col.tag.Equals("Player"))
        {
            col.GetComponent<Player>().EnableTripleShot();
            Destroy(gameObject);
        }
    }
}
