using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image livesImage;
    [SerializeField] private GameObject gameOverTextFolder;
    
    [Header("Other")]
    [SerializeField] private Sprite[] livesSprites;
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        scoreText.text = "0";
    }

    public void UpdateScoreText(long score)
    {
        scoreText.text = score.ToString();
    }
    public void UpdateLivesImage(int currentLives)
    {
        livesImage.sprite = livesSprites[currentLives];
        if (currentLives == 0)
        {
            gameOverTextFolder.SetActive(true);
        }
    }
}
