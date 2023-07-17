using UnityEngine;

public class PunchDamage : MonoBehaviour
{
    public int damageAmount = 2; // The amount of damage the weapon deals

    private EnemyHealth enemyHealth; // Reference to the EnemyHealth script
    private bool isDamaging = false; // Flag to prevent continuous damage

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        if (other.CompareTag("Enemy") && !isDamaging)
        {
            isDamaging = true; // Set the flag to true to prevent continuous damage

            // Get the EnemyHealth component from the collided object
            enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collided object is an enemy and was previously damaging
        if (other.CompareTag("Enemy") && isDamaging)
        {
            isDamaging = false; // Reset the flag to allow damage again

            // Deal damage to the enemy
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Enemy Attacked");
            }
        }
    }
}

