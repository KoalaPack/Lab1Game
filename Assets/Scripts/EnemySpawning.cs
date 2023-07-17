using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public GameObject[] spawnPoints; // An array of spawn point GameObjects
    public float timeBetweenWaves = 2f; // Time between waves in seconds

    private int currentWave = 1; // The current wave number
    private int enemiesInWave = 1; // The number of enemies to spawn in the current wave
    private bool isSpawningWave = false; // Flag to check if a wave is currently spawning

    private void Start()
    {
        StartNewWave();
    }

    private void Update()
    {
        if (!isSpawningWave && AreEnemiesDefeated())
        {
            StartCoroutine(StartNewWaveWithDelay(timeBetweenWaves));
        }
    }

    private bool AreEnemiesDefeated()
    {
        // Check if there are no enemies left in the scene
        bool noEnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length == 1;
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
        Debug.Log("Starting Wave " + currentWave + " with " + enemiesInWave + " enemies.");

        // Spawn enemies for the current wave
        for (int i = 0; i < enemiesInWave; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomIndex].transform.position;
            Quaternion spawnRotation = spawnPoints[randomIndex].transform.rotation;

            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }

        currentWave++;
    }
}
