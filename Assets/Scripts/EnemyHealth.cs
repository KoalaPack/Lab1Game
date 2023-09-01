using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    // Enemy health
    public int maxHealth = 3; // The maximum health of the enemy
    public int currentHealth; // The current health of the enemy

    public List<MeshRenderer> flashWhiteColour;

    public Material hitMat;

    public Material[] materialVariants; // Array of different material variants

    private int currentMaterialIndex = 0; // Index of the currently selected material

    private Renderer render; // Reference to the object's renderer
    // Enum to represent different material variants
    public enum MaterialVariant
    {
        Red,
        Blue,
        Yellow
    }
    public MaterialVariant currentMaterial;
    public EnemyLevel enemyLevel;
    public enum EnemyLevel
    {
        Red,
        Blue,
        Yellow
    }


    public GameObject regularEnemy;



    public GameObject enemyAnimationController;

    public GameObject EnemyExplodeObject;

    //public GameObject enemyAiScript;
    public EnemyAi enemyAiScript;

    //Enemy object to be destroyed
    public GameObject prefabGameObject;

    private NavMeshAgent navMeshAgent;


    public float speedMultiplier = 2.0f;
    public float pauseDuration = 1f;

    // All audio References 
    public AudioSource audioSource;  // Reference to the Audio Source
    public AudioClip hitSound;       // Sound to be played when hit
    public AudioSource audiobreak;  // Reference to the Audio Source
    public AudioClip deadSound;       // Sound to be played when hit


    private void Start()
    {
        currentHealth = maxHealth; // Set the initial health to the maximum
        regularEnemy.SetActive(true);
        EnemyExplodeObject.SetActive(false);
        enemyAiScript.enabled = true;
        enemyAnimationController = GameObject.Find("EnemyExplode");

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        //Get the material chosen
        render = GetComponent<Renderer>();

        currentMaterialIndex = (int)currentMaterial; // Set the index based on the currentMaterial enum value
        UpdateMaterial(); // Set the initial material

        //Enemy Health Level selection
        SetMaxHealthFromEnum();
        MaterialStartup();

    }


    private void UpdateMaterial()
    {
        switch (currentMaterial)
        {
            case MaterialVariant.Red:
                render.material = materialVariants[0]; // Assuming the first material is red
                break;
            case MaterialVariant.Blue:
                render.material = materialVariants[1]; // Assuming the second material is blue
                break;
            case MaterialVariant.Yellow:
                render.material = materialVariants[2]; // Assuming the third material is yellow
                break;
        }
    }

    private void SetMaxHealthFromEnum()
    {
        switch (enemyLevel)
        {
            case EnemyLevel.Red:
                currentHealth = 3; // Set the appropriate value for the Red level
                break;
            case EnemyLevel.Blue:
                currentHealth = 2; // Set the appropriate value for the Blue level
                break;
            case EnemyLevel.Yellow:
                currentHealth = 1; // Set the appropriate value for the Yellow level
                break;
            default:
                currentHealth = 3; // Default value
                break;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount; // Decrease the current health by the damage amount

            if (currentHealth == 2)
            {
                currentMaterial = MaterialVariant.Blue; // Change to blue variant
                currentMaterialIndex = 1; // Update the index for the blue material
            }
            else if (currentHealth == 1)
            {
                currentMaterial = MaterialVariant.Yellow; // Change to yellow variant
                currentMaterialIndex = 2; // Update the index for the yellow material
            }
            UpdateMaterial(); // Update the material based on the currentMaterial value

            // Play the hit sound
            audioSource.PlayOneShot(hitSound);
        }
        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            regularEnemy.SetActive(false);
            EnemyExplodeObject.SetActive(true);
            enemyAiScript.enabled = false;
            navMeshAgent.enabled = false;
            // Start the fading coroutine
            //StartCoroutine(FadeMaterial());
            audioSource.PlayOneShot(deadSound);
            StartCoroutine(PauseBeforeDeathCoroutine());
            Animator enemyAnimator = enemyAnimationController.GetComponent<Animator>();
            enemyAnimator.speed = speedMultiplier;
            enemyAnimator.Play("EnemyExplodeAnim");


            // Turn on isKinematic while the animation plays
            Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
            enemyRigidbody.isKinematic = true;

        }
    }

    public void MaterialStartup()
    {
        switch (currentMaterial)
        {
            case MaterialVariant.Red:
                currentMaterialIndex = 0;
                break;
            case MaterialVariant.Blue:
                currentMaterialIndex = 1;
                break;
            case MaterialVariant.Yellow:
                currentMaterialIndex = 2;
                break;
            default:
                currentMaterialIndex = 0; // Default to Red if the material is not recognized
                break;
        }

        // Set the material based on the currentMaterialIndex
        render.material = materialVariants[currentMaterialIndex];
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
        navMeshAgent.isStopped = true;
        // Destroy the enemy GameObject
        Destroy(prefabGameObject);
    }

    IEnumerator WaitForMatChange()
    {
        // Store the original materials
        List<Material> originalMaterials = new List<Material>();
        foreach (MeshRenderer renderer in flashWhiteColour)
        {
            originalMaterials.Add(renderer.material);
            renderer.material = hitMat;
        }

        yield return new WaitForSeconds(0.1f);

        // Restore the original materials and switch to the next material variant
        for (int i = 0; i < flashWhiteColour.Count; i++)
        {
            flashWhiteColour[i].material = materialVariants[currentMaterialIndex]; // Use the currentMaterialIndex to select the material from the array
        }
    }

    public void ChangeEnemyMat()
    {

        StartCoroutine(WaitForMatChange());
    }
}
