using UnityEngine;

public class Health : MonoBehaviour
{
    // Delegate to handle death event
    public delegate void DeathEvent();
    public static event DeathEvent OnDeath;

    [SerializeField] private int maxHealth = 100; // Maximum health value for the object
    private int currentHealth; // Current health value for the object

    // Public property for accessing the current health value
    public int CurrentHealth => currentHealth;

    // Public property for accessing the maximum health value
    public int MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maxHealth when the object starts
    }

    // Function to take damage
    public void TakeDamage(int damageAmount)
    {
        if (damageAmount < 0)
        {
            Debug.LogWarning("Damage amount should be a positive value. Use Heal method to heal.");
            return;
        }

        currentHealth -= damageAmount; // Reduce current health by the damage amount

        // Check if health has dropped below 0 (dead)
        if (currentHealth <= 0)
        {
            Die(); // Call the Die function if the object's health is less than or equal to 0
        }
    }

    // Function to heal
    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            Debug.LogWarning("Heal amount should be a positive value. Use TakeDamage method to take damage.");
            return;
        }

        currentHealth += healAmount; // Increase current health by the heal amount

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
