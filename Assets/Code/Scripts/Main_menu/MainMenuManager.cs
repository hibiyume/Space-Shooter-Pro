using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private int gameSceneId;

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneId);
    } 
}
