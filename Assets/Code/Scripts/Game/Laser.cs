using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Laser Parameters")]
    [SerializeField] private float speed = 8f;

    private void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));

        if (transform.position.y > 8f)
        {
            if (transform.parent)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }
}