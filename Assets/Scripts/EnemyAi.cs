using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public Transform target; // The target the enemy will move towards and attack
    public float movementSpeed = 5f; // The movement speed of the enemy
    public float attackRange = 2f; // The range at which the enemy can attack the target
    public int attackDamage = 10; // The amount of damage the enemy inflicts on the target
    public float attackForce = 10f; // The force applied to the target on attack

    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component


    private bool canAttack = true; // Flag to indicate if the enemy can attack
    private bool isStopped = false; // Flag to indicate if the enemy is currently stopped

    public float attackTimer = 0f; // Timer to track the cooldown duration
    public float attackCooldown = 2f; // The cooldown duration between attacks
    public float stopDuration = 1f; // The duration the enemy stops before resuming movement


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // If within attack range, initiate an attack
            if (distanceToTarget <= attackRange && canAttack)
            {
                Attack();
            }
            else
            {
                // Set the destination of the NavMeshAgent to the target's position
                navMeshAgent.SetDestination(target.position);


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
            attackTimer += Time.deltaTime;

            // Check if the cooldown period has elapsed
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0f;
            }
        }
    }

    private void Attack()
    {
        // Perform attack logic here
        Debug.Log("Enemy attacks the target!");

        // Apply force to the target
        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
        if (targetRigidbody != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            targetRigidbody.AddForce(direction * attackForce, ForceMode.Impulse);
        }
        canAttack = false;

        // Stop the enemy's movement
        StopMovement();
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
}