using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void DeathEvent();
    public static event DeathEvent OnDeath;

    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Function to take damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if health has dropped below 0 (dead)
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to heal
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Make sure currentHealth doesn't exceed maxHealth
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void Die()
    {
        // Handle death event (e.g., respawn, game over, etc.)
        Debug.Log(gameObject.name + " has died!");

        // Call functions from other scripts subscribed to the OnDeath event
        OnDeath?.Invoke();

        // Implement your custom death behavior here (e.g., respawn, game over logic).
    }
}
