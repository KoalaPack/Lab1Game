using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasController : MonoBehaviour
{
    public GameObject GameScreen;
    public GameObject PlayAgainScreen;
    public GameObject YouLose;
    public GameObject Player;

    public float menuTimeDelay = 2f; // Time between waves in seconds

    private bool methodCalled = false; // Flag to track if the method has been called

    // Pause Menu
    public GameObject PauseScreen;
    public GameObject OptionsScreen;

    // Timer
    public TMP_Text timerText;
    private Timer timer; //Reference to timer script


    // Score multiplier
    public TMP_Text scoreText;
    private float scoreTotal = 0f;



    // Waves
    public TMP_Text wavesText;
    private EnemySpawning waveValue; //Reference to timer script



    // Start is called before the first frame update
    void Start()
    {
        GameScreen.SetActive(true);

        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        // End Game Menu
        PlayAgainScreen.SetActive(false);
        YouLose.SetActive(false);

        // Pause Menu
        PauseScreen.SetActive(false);
        OptionsScreen.SetActive(false);

        // Score 
        scoreTotal = 0f;

        // Waves
        GameObject EnemySpawning = GameObject.Find("EnemySpawning");
        waveValue = EnemySpawning.GetComponent<EnemySpawning>();

        int value = waveValue.currentWave;
        Debug.Log("wave" + value);
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F0");

        if (Player == null && !methodCalled)
        {
            GameScreen.SetActive(false);
            YouLose.SetActive(true);
            StartCoroutine(WaitAndContinue());
            methodCalled = true;

            //Stop the timer
            timer.StopTimer();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameScreen.SetActive(false);
            PauseScreen.SetActive(true);
        }

        wavesText.text = "Wave: " + waveValue;
        scoreText.text = "Score: " + scoreTotal;
    }

    private System.Collections.IEnumerator WaitAndContinue()
    {
        // Wait for 5 seconds before continuing
        yield return new WaitForSeconds(5.0f);
        PlayAgain();
    }

    private void PlayAgain()
    {
        YouLose.SetActive(false);
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

    // Pause Button Below vvv
    // Resume Button to resume the game
    public void Resume()
    {

    }

    // Options button to view keybinds and audio
    public void Options()
    {

    }

    // Main menu button
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

