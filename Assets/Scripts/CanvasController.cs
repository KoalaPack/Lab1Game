using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasController : MonoBehaviour
{
    public GameObject GameScreen;
    public GameObject PlayAgainScreen;
    public GameObject YouLoose;
    public GameObject Player;

    public float menuTimeDelay = 2f; // Time between waves in seconds

    private bool methodCalled = false; // Flag to track if the method has been called

    // Start is called before the first frame update
    void Start()
    {
        GameScreen.SetActive(true);
        PlayAgainScreen.SetActive(false);
        YouLoose.SetActive(false);
    }

    private void Update()
    {

        if (Player == null && !methodCalled)
        {
            GameScreen.SetActive(false);
            YouLoose.SetActive(true);
            StartCoroutine(WaitAndContinue());
            methodCalled = true;
        }
    }

    private System.Collections.IEnumerator WaitAndContinue()
    {
        // Wait for 5 seconds before continuing
        yield return new WaitForSeconds(5.0f);
        PlayAgain();
    }

    private void PlayAgain()
    {
        YouLoose.SetActive(false);
        PlayAgainScreen.SetActive(true);
    }

    public void Yes()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void No()
    {
        SceneManager.LoadScene("MainMenu");
    }


    //Wait 5 seconds and then display would you like to play again menu
    //If the player wants to play again then restart the scene
    //Else If the player doesnt call the script to change scenes

}

