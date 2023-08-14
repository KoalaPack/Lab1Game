using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
using System.Threading;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public GameObject[] spawnPoints; // An array of spawn point GameObjects
    public float timeBetweenWaves = 3.5f; // Time between waves in seconds

    public int currentWave = 1; // The current wave number
    private int enemiesInWave = 1; // The number of enemies to spawn in the current wave
    private bool isSpawningWave = false; // Flag to check if a wave is currently spawning

    public TMP_Text wavesText;
    public TMP_Text waveWaitText;
    public Animator wavesAnim;
    public int waveTotal = 1;

    public TMP_Text scoreText;
    public int scoreTotal = 0;

    private void Start()
    {
        wavesAnim.Play("WaveInAndOut"); // Plays wave animation
        wavesText.text = "Wave: " + waveTotal ;
        waveWaitText.text = "Wave: " + currentWave;

        scoreTotal = 0;
        StartWait();



    }

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(3f);
        StartNewWave();
    }

    private void Update()
    {
        if (!isSpawningWave && AreEnemiesDefeated())
        {
            Debug.Log("animationplaying");
            wavesAnim.Play("WaveInAndOut"); // Plays wave animation
            waveTotal ++;
            wavesText.text = "Wave: " + currentWave;
            waveWaitText.text = "Wave: " + currentWave;
            StartCoroutine(StartNewWaveWithDelay(timeBetweenWaves));
        }



    }

    private bool AreEnemiesDefeated()
    {
        // Check if there are no enemies left in the scene
        bool noEnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;


        return noEnemiesLeft;
    }

    private IEnumerator StartNewWaveWithDelay(float delayInSeconds)
    {
        isSpawningWave = true;
        Debug.Log("Waiting for " + delayInSeconds + " seconds before starting the new wave...");
        yield return new WaitForSeconds(delayInSeconds);
        StartNewWave();
        isSpawningWave = false;
    }

    private void StartNewWave()
    {
        // Double the number of enemies for the next wave
        enemiesInWave = currentWave;
        enemiesInWave *= 2;

        // Spawn enemies for the current wave
        for (int i = 0; i < enemiesInWave; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomIndex].transform.position;
            Quaternion spawnRotation = spawnPoints[randomIndex].transform.rotation;

            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }
        currentWave++;

        scoreTotal = currentWave;
        scoreText.text = "Wave: " + scoreTotal;
    }
}
