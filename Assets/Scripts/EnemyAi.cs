using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public float movementSpeed = 5f; // The movement speed of the enemy
    public float attackRange = 2f; // The range at which the enemy can attack the target
    public int attackDamage = 10; // The amount of damage the enemy inflicts on the target
    public float attackForce = 10f; // The force applied to the target on attack

    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    private bool canAttack = true; // Flag to indicate if the enemy can attack
    private bool isStopped = false; // Flag to indicate if the enemy is currently stopped

    public float attackTimer = 3f; // Timer to track the cooldown duration
    public float attackCooldown = 2f; // The cooldown duration between attacks
    public float stopDuration = 1f; // The duration the enemy stops before resuming movement

    public string targetTag = "Player"; // The tag of the target to follow and attack

    private EnemyHealth healthValue;

    private Rigidbody rb;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

        healthValue = GetComponent<EnemyHealth>();
        int value = healthValue.currentHealth;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        // Find the target using the specified tag
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);

            // If within attack range, initiate an attack
            if (distanceToTarget <= attackRange && canAttack)
            {
                Attack();
            }
            else
            {
                // Set the destination of the NavMeshAgent to the target's position
                navMeshAgent.SetDestination(targetObject.transform.position);

                // Check if the enemy is currently stopped
                if (isStopped)
                {
                    // Resume movement after the stop duration
                    StartCoroutine(ResumeMovement(stopDuration));
                }
            }
        }

        // Update the attack timer
        if (!canAttack)
        {
            //Rigidbody is switched on 
            rb.isKinematic = false;

            //cooldown timer starts
            attackTimer += Time.deltaTime;

            // Check if the cooldown period has elapsed
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 3f;
            }
        }
        CheckEnemyHealth();
    }

    private void Attack()
    {
        // Perform attack logic here
        // Debug.Log("Enemy attacks the target!");

        // Apply force to the target
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            Rigidbody targetRigidbody = targetObject.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                Vector3 direction = (targetObject.transform.position - transform.position).normalized;
                targetRigidbody.AddForce(direction * attackForce, ForceMode.Impulse);
            }
        }
        canAttack = false;

        // Stop the enemy's movement
        StopMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If attack tags triggers enemy tag stop the movement of the enemy for .3 seconds
        if (other.CompareTag("Attack"))
        {
            Debug.Log("Enemy Stun");
            navMeshAgent.isStopped = true;
            StartCoroutine(WaitAndContinue());
        }
    }

    private IEnumerator WaitAndContinue()
    {
        // Wait for .5 seconds before continuing
        yield return new WaitForSeconds(0.5f);
        navMeshAgent.isStopped = false;
    }

    private void StopMovement()
    {
        // Stop the enemy's movement
        navMeshAgent.isStopped = true;

        // Set the flag to indicate that the enemy is currently stopped
        isStopped = true;
    }

    private IEnumerator ResumeMovement(float duration)
    {
        // Wait for the stop duration
        yield return new WaitForSeconds(duration);

        // Resume the enemy's movement
        navMeshAgent.isStopped = false;

        // Reset the flag to indicate that the enemy is no longer stopped
        isStopped = false;

        // Enable the ability to attack again
        canAttack = true;
    }

    private void CheckEnemyHealth()
    {
        if (healthValue = null)
        {
            Debug.Log("Enemyhealth 0");
        }
    }
}
