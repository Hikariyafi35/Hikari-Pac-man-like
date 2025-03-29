using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("BlockoutWarehouse");
    }
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
