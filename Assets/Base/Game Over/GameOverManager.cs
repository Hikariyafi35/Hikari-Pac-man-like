using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private string _gameplaySceneName;
    [SerializeField]
    private string _mainmenuSceneName;
    private void Start() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;    
    }
    public void Retry()
    {
        SceneManager.LoadScene(_gameplaySceneName);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(_mainmenuSceneName);
    }
}
