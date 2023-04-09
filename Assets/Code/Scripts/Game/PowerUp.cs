using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Power Up Parameters")]
    [SerializeField] private PowerUpTypes powerUpType;
    [SerializeField] private float movementSpeed;
    
    [Header("Audio")]
    [SerializeField] private AudioClip powerUpPickupAudioClip;
    
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

            AudioSource.PlayClipAtPoint(powerUpPickupAudioClip, transform.position);
            
            Destroy(gameObject);
            /*Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(gameObject, 2.5f);*/
        }
    }
}