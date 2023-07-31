using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Different menues
    public GameObject mainMenu;
    public GameObject options;

    //Setting all scenes to their start position
    public void Start()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    //Deactivates the menu UI layer and activates the options layer
    public void OptionButton()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
    //Deactivates the menu UI layer and activates the level select layer
    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void backToMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    //Will change our scene to the string passed in 
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    //Reloads the current scene we are in
    public void RelodeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Loads Main Menu Scene
    public void ToTitleGame()
    {
        SceneManager.LoadScene("Title");
    }

    //Loads Normal Game Scene
    public void ToNormalGame()
    {
        SceneManager.LoadScene("NormalGame");
    }

    //Loads Tron Game Scene
    public void ToTronGame()
    {
        SceneManager.LoadScene("TronGame");
    }

    //Gets the active scene name
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //Quit Button
    public void QuitGame()
    {
        Application.Quit();
    }
}
