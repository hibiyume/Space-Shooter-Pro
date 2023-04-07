using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpTypes powerUpType;
    [SerializeField] private float movementSpeed;
    private enum PowerUpTypes
    {
        TripleShot,
        Speed,
        Shield
    }

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

            Destroy(gameObject);
        }
    }
}