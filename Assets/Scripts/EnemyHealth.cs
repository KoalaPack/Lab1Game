using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // The maximum health of the enemy
    private int currentHealth; // The current health of the enemy

    public List<MeshRenderer> meshColours;

    public Material regularMat;
    public Material hitMat;

    public GameObject regularEnemy;
    public GameObject explodeEnemy;

    public Animator enemyExplode;

    private void Start()
    {
        currentHealth = maxHealth; // Set the initial health to the maximum
        regularEnemy.SetActive(true);
        explodeEnemy.SetActive(false);
        enemyExplode = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Decrease the current health by the damage amount

        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            regularEnemy.SetActive(false);
            explodeEnemy.SetActive(true);
            enemyExplode.Play("EnemyExplodeAnim");
            StartCoroutine(pauseBeforeDeath());

            Die();
        }
    }

    private IEnumerator pauseBeforeDeath()
    {
        // Wait for the stop duration
        yield return new WaitForSeconds(3f);
    }

    private void Die()
    {
        Debug.Log("Enemy has been defeated!");

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
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
