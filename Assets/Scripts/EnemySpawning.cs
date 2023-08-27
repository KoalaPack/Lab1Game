using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class EnemySpawning : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPoints;
    public float timeBetweenWaves = 3.5f;

    public int currentWave = 1;
    private int enemiesInWave = 1;
    private bool isSpawningWave = false;

    public TMP_Text wavesText;
    public TMP_Text waveWaitText;
    public Animator wavesAnim;
    public int waveTotal = 1;

    public TMP_Text scoreText;
    public int scoreTotal = 0;

    public float baseTimeLimit = 10.0f;
    public float timeLimitIncreasePerWave = 2.0f;

    private float waveStartTime;
    private float currentWaveTimeLimit;

    private void Start()
    {
        wavesAnim.Play("WaveInAndOut");
        wavesText.text = "Wave: " + waveTotal;
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
            wavesAnim.Play("WaveInAndOut");
            waveTotal++;
            wavesText.text = "Wave: " + currentWave;
            waveWaitText.text = "Wave: " + currentWave;
            StartCoroutine(StartNewWaveWithDelay(timeBetweenWaves));
        }
    }

    private bool AreEnemiesDefeated()
    {
        bool noEnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;

        if (noEnemiesLeft && isSpawningWave)
        {
            CalculateScore();
        }

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
        enemiesInWave = currentWave;
        enemiesInWave *= 2;

        for (int i = 0; i < enemiesInWave; i++)
        {
            int randomPrefabIndex = Random.Range(0, enemyPrefabs.Length);
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomSpawnIndex].transform.position;
            Quaternion spawnRotation = spawnPoints[randomSpawnIndex].transform.rotation;

            GameObject selectedPrefab = enemyPrefabs[randomPrefabIndex];
            Instantiate(selectedPrefab, spawnPosition, spawnRotation);
        }
        currentWave++;

        // Update wave timer and time limit
        currentWaveTimeLimit = baseTimeLimit + (currentWave - 1) * timeLimitIncreasePerWave;
        waveStartTime = Time.time;

        // Calculate score for the previous wave and add it to the current score
        CalculateScore();
        scoreText.text = "Score: " + scoreTotal.ToString();
        waveWaitText.text = "Wave: " + currentWave;
    }

    private void CalculateScore()
    {
        float waveCompletionTime = Time.time - waveStartTime;

        if (waveCompletionTime <= currentWaveTimeLimit)
        {
            scoreTotal += 100;
        }
        else
        {
            float timeDifference = waveCompletionTime - currentWaveTimeLimit;
            int timeScore = Mathf.Max(0, Mathf.FloorToInt(100 - (timeDifference * 10) - (currentWave * 5)));
            scoreTotal += timeScore;
        }

        scoreText.text = "Score: " + scoreTotal.ToString();
    }
}
