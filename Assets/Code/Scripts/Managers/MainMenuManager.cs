using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private int singleplayerGameSceneId;
    [SerializeField] private int coopGameSceneId;

    public void LoadSinglePlayerGameScene()
    {
        SceneManager.LoadScene(singleplayerGameSceneId);
    }
    
    public void LoadCoopGameScene()
    {
        SceneManager.LoadScene(coopGameSceneId);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
