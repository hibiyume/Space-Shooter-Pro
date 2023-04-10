using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Parameters")]
    [SerializeField] private long playerScore = 0;
    public bool IsPlayerAlive { get; private set; } = true;
    
    [Header("Other")]
    [SerializeField] private int gameSceneId;
    private UIManager _uiManager;
    
    [Header("Input Controls")]
    [SerializeField] private InputAction restartLevel;

    private void OnDisable()
    {
        restartLevel.Disable();
    }

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }
    private void Update()
    {
        if (restartLevel.triggered)
        {
            SceneManager.LoadScene(gameSceneId); // Current game scene
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        _uiManager.UpdateScoreText(playerScore);
    }
    
    public void OnGameOver()
    {
        IsPlayerAlive = false;
        restartLevel.Enable();
    }
}
