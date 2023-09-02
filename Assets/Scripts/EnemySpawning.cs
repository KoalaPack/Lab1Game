using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class EnemySpawning : MonoBehaviour
{
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

    public enum EnemyLevel
    {
        Red,
        Blue,
        Yellow
    }

    public GameObject defaultObject;
    public GameObject prefabOne;
    public GameObject prefabTwo;
    public GameObject prefabThree;

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


        if (currentWave >= 1 && currentWave <= 4)
        {
            defaultObject = prefabOne;
        }
        else if (currentWave > 4 && currentWave <= 9)
        {
            defaultObject = prefabTwo;
        }
        else if (currentWave > 9 && currentWave <= 14)
        {
            defaultObject = prefabThree;
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
        if (currentWave <= 2)
        {
            scoreTotal += 2;
            for (int i = 0; i < enemiesInWave; i++)
            {
                // Use the defaultObject instead of random selection
                GameObject selectedPrefab = prefabOne;

                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Vector3 spawnPosition = spawnPoints[randomSpawnIndex].transform.position;
                Quaternion spawnRotation = spawnPoints[randomSpawnIndex].transform.rotation;

                Instantiate(selectedPrefab, spawnPosition, spawnRotation);
            }
        }
        else if (currentWave >= 3 && currentWave <= 5)
        {
            scoreTotal += 10;
            for (int i = 0; i < enemiesInWave; i++)
            {
                // Select a random prefab between prefabOne and prefabTwo
                GameObject selectedPrefab = (Random.Range(0, 2) == 0) ? prefabOne : prefabTwo;

                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Vector3 spawnPosition = spawnPoints[randomSpawnIndex].transform.position;
                Quaternion spawnRotation = spawnPoints[randomSpawnIndex].transform.rotation;

                Instantiate(selectedPrefab, spawnPosition, spawnRotation);
            }
        }
        else if (currentWave >= 6 && currentWave <= 8)
        {
            scoreTotal += 15;
            for (int i = 0; i < enemiesInWave; i++)
            {
                // Select a random prefab between prefabOne and prefabTwo
                GameObject selectedPrefab = (Random.Range(0, 2) == 0) ? prefabThree : prefabTwo;

                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Vector3 spawnPosition = spawnPoints[randomSpawnIndex].transform.position;
                Quaternion spawnRotation = spawnPoints[randomSpawnIndex].transform.rotation;

                Instantiate(selectedPrefab, spawnPosition, spawnRotation);
            }
        }
        else if (currentWave > 9)
        {
            scoreTotal += 20;
            for (int i = 0; i < enemiesInWave; i++)
            {
                // Select a random prefab between prefabOne, prefabTwo, and prefabThree
                int randomPrefabIndex = Random.Range(0, 3);
                GameObject selectedPrefab;

                if (randomPrefabIndex == 0)
                {
                    selectedPrefab = prefabOne;
                }
                else if (randomPrefabIndex == 1)
                {
                    selectedPrefab = prefabTwo;
                }
                else
                {
                    selectedPrefab = prefabThree;
                }

                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Vector3 spawnPosition = spawnPoints[randomSpawnIndex].transform.position;
                Quaternion spawnRotation = spawnPoints[randomSpawnIndex].transform.rotation;

                Instantiate(selectedPrefab, spawnPosition, spawnRotation);
            }
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


        float timeDifference = waveCompletionTime - currentWaveTimeLimit;
        int timeScore = Mathf.Max(0, Mathf.FloorToInt(10 - (timeDifference * 10) - (currentWave * 5)));
        scoreTotal += timeScore;

        if (currentWave == 2)
        {
            scoreTotal = 0;
            scoreText.text = "Score: " + scoreTotal.ToString();
        }
        scoreText.text = "Score: " + scoreTotal.ToString();
    }
}