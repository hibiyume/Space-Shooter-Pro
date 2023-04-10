using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio Manager");
        
        if (objs.Length > 1)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
}
