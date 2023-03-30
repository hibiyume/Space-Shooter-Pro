using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Laser : MonoBehaviour
{
    [Header("Laser Parameters")]
    [SerializeField] private float speed = 8f;
    
    private void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));

        if (transform.position.y > 8f)
            Destroy(gameObject);
    }
}
