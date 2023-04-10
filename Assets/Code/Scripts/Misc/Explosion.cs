using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _audioSource.Play();
        StartCoroutine(DestroyExplosion());
    }

    private IEnumerator DestroyExplosion()
    {
        yield return new WaitUntil(() => !_audioSource.isPlaying);
        Destroy(gameObject);
    }
}
