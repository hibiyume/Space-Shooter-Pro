using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image player1livesImage;
    [SerializeField] private Image player2livesImage;
    [SerializeField] private GameObject gameOverTextFolder;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    
    [Header("Other")]
    [SerializeField] private Sprite[] livesSprites;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        if (!_gameManager.IsCoopMode())
            highscoreText.text = "High score : " + PlayerPrefs.GetInt("Highscore").ToString();
        scoreText.text = "0";
    }

    public void UpdateScoreText(long score)
    {
        scoreText.text = score.ToString();
    }
    public void UpdateLivesImage(int currentLives, string playerTag)
    {
        if (playerTag.Equals("Player1"))
            player1livesImage.sprite = livesSprites[currentLives];
        else if (playerTag.Equals("Player2"))
            player2livesImage.sprite = livesSprites[currentLives];
    }

    public void ShowGameOverContent()
    {
        gameOverScoreText.text = $"Your score : {scoreText.text}";
        gameOverTextFolder.SetActive(true);
    }
}
