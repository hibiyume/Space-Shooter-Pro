using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Parameters")]
    [SerializeField] private int playerScore = 0;
    [SerializeField] private bool isCoopMode;
    public bool IsCoopMode() { return isCoopMode; }
    public bool IsGamePaused { get; private set; }
    public bool ArePlayersAlive { get; private set; } = true;
    private bool _isPlayer1Alive = true;
    private bool _isPlayer2Alive = true;

    [Header("Other")]
    [SerializeField] private GameObject pauseGameFolder;
    [SerializeField] private int mainMenuSceneId;
    [SerializeField] private int gameSceneId;
    private UIManager _uiManager;

    [Header("Input Controls")]
    [SerializeField] private InputAction restartLevel;
    [SerializeField] private InputAction pauseGame;

    private void OnEnable()
    {
        pauseGame.Enable();
    }
    private void OnDisable()
    {
        restartLevel.Disable();
        pauseGame.Disable();
    }

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        Cursor.visible = false;
    }
    private void Update()
    {
        if (!IsGamePaused)
        {
            if (restartLevel.triggered)
            {
                SceneManager.LoadScene(gameSceneId); // Current game scene
            }
        }

        if (pauseGame.triggered)
        {
            PauseOrUnpauseGame();
        }
    }

    private void OnGameOver()
    {
        ArePlayersAlive = false;
        _uiManager.ShowGameOverContent();
        restartLevel.Enable();

        if (!isCoopMode)
            SaveHighscore();
    }
    private void SaveHighscore()
    {
        if (PlayerPrefs.GetInt("Highscore") < playerScore)
            PlayerPrefs.SetInt("Highscore", playerScore);
    }

    public void PauseOrUnpauseGame()
    {
        if (!pauseGameFolder.activeSelf)
        {
            Time.timeScale = 0f;
            IsGamePaused = true;
            pauseGameFolder.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            IsGamePaused = false;
            pauseGameFolder.SetActive(false);
            Cursor.visible = false;
        }
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneId);
    }
    public void AddScore(int score)
    {
        playerScore += score;
        _uiManager.UpdateScoreText(playerScore);
    }
    public void OnPlayerDeath(string playerTag)
    {
        if (playerTag.Equals("Player1"))
        {
            _isPlayer1Alive = false;
        }
        else if (playerTag.Equals("Player2"))
            _isPlayer2Alive = false;

        if ((!_isPlayer1Alive && !_isPlayer2Alive) || !isCoopMode)
        {
            OnGameOver();
        }
    }
}