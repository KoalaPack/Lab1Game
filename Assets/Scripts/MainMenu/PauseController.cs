using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");


    }

    public void RelodeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
    