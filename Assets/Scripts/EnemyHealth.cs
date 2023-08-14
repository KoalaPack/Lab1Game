using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // The maximum health of the enemy
    public int currentHealth; // The current health of the enemy

    public List<MeshRenderer> meshColours;

    public Material regularMat;
    public Material hitMat;

    public GameObject regularEnemy;

    public float pauseDuration = 1f;

    public GameObject enemyAnimationController;

    public GameObject EnemyExplodeObject;

    //public GameObject enemyAiScript;
    public EnemyAi enemyAiScript;

    //Enemy object to be destroyed
    public GameObject prefabGameObject;


    private void Start()
    {
        currentHealth = maxHealth; // Set the initial health to the maximum
        regularEnemy.SetActive(true);
        EnemyExplodeObject.SetActive(false);
        enemyAiScript.enabled = true;
        enemyAnimationController = GameObject.Find("EnemyExplode");

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth >= 0)
        {
            currentHealth -= damageAmount; // Decrease the current health by the damage amount
        }
        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            regularEnemy.SetActive(false);
            EnemyExplodeObject.SetActive(true);
            enemyAiScript.enabled = false;
            // Start the fading coroutine
            //StartCoroutine(FadeMaterial());

            StartCoroutine(PauseBeforeDeathCoroutine());

            enemyAnimationController.GetComponent<Animator>().Play("EnemyExplodeAnim");
        }
    }

    private IEnumerator PauseBeforeDeathCoroutine()
    {
        // Wait for the specified duration before calling Die()
        yield return new WaitForSeconds(pauseDuration);
        Die();
    }

    private void Die()
    {
        Debug.Log("Enemy has been defeated!");

        // Destroy the enemy GameObject
        Destroy(prefabGameObject);
    }

    // Enemy hit Colour changer
    IEnumerator WaitForMatChange()
    {
        foreach(MeshRenderer render in meshColours)
        {
            render.material = hitMat;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (MeshRenderer render in meshColours)
        {
            render.material = regularMat;
        }
        
    }
    public void ChangeEnemyMat()
    {
        StartCoroutine(WaitForMatChange());
    }
}
