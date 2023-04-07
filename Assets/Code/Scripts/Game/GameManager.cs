using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Parameters")]
    private bool _isGameOver;

    [Header("Other")]
    [SerializeField] private int gameSceneId;
    
    [Header("Input Controls")]
    [SerializeField] private InputAction restartLevel;

    private void OnDisable()
    {
        restartLevel.Disable();
    }
    
    private void Update()
    {
        if (restartLevel.triggered)
        {
            SceneManager.LoadScene(gameSceneId); // Current game scene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
        restartLevel.Enable();
    }
}
