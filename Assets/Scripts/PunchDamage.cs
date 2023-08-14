using UnityEngine;

public class PunchDamage : MonoBehaviour
{

    public int damageAmount = 2; // The amount of damage the weapon deals

    private EnemyHealth enemyHealth; // Reference to the EnemyHealth script
    private bool isDamaging = false; // Flag to prevent continuous damage

    public float attackForce = 1f;


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
            other.GetComponent<EnemyHealth>().ChangeEnemyMat();
            isDamaging = false; // Reset the flag to allow damage again

            // Deal damage to the enemy
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Enemy Attacked");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an enemy (the attacked object)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                // Calculate the force direction (from the attacker to the enemy)
                Vector3 forceDirection = (collision.transform.position - transform.position).normalized;

                // Apply force to the enemy
                enemyRigidbody.AddForce(forceDirection * attackForce, ForceMode.Impulse);
            }
        }
    }
}

