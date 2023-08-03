using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    //Different menues
    public GameObject mainMenu;

    //Get the Animation
    public Animator animOpen;
    public Animator animClose;
    private bool optionButtonClicked = false;
    private bool optionsButton = false;
    public GameObject Music;
    public GameObject SFX;

    //Setting all scenes to their start position
    public void Start()
    {
        mainMenu.SetActive(true);
        optionButtonClicked = false;
        animOpen = GetComponent<Animator>();
        animClose = GetComponent<Animator>();
        Music.SetActive(false);
        SFX.SetActive(false);
        optionsButton = false;
    }

    public void Options()
    {
        optionsButton = true;
        Debug.Log("Options button clicked");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (optionsButton == true && optionButtonClicked == false)
        {
            optionsButton = false;
            Music.SetActive(true);
            SFX.SetActive(true);
            animOpen.Play("OptionsOpen");
            Debug.Log("Animation open");
            optionButtonClicked = true;
        }
        if (optionsButton == true && optionButtonClicked == true)
        {
            optionsButton = false;
            animClose.Play("OptionsClose");
            Debug.Log("Animation close");
            optionButtonClicked = false;
        }
    }

    //private System.Collections.IEnumerator WaitAndContinue()
    //{

    //    yield return new WaitForSeconds(1f);
    //    AbleToClickOptionsButton();
    //}

    //private void AbleToClickOptionsButton()
    //{
    //    optionButtonClicked = true;
    //}

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
