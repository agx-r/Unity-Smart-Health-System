using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HealthChangeEvent(int currentHealth, int maxHealth); // Delegate to handle health change event
    public static event HealthChangeEvent OnHealthChange; // Event triggered when health changes

    [SerializeField] private int maxHealth = 100; // Maximum health value for the object
    private int currentHealth; // Current health value for the object

    public int CurrentHealth => currentHealth; // Public property for accessing the current health value
    public int MaxHealth => maxHealth; // Public property for accessing the maximum health value

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maxHealth when the object starts
    }

    /// <summary>
    /// Function to apply damage to the object's health.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to apply.</param>
    public void TakeDamage(int damageAmount)
    {
        if (damageAmount <= 0)
        {
            Debug.LogWarning("Damage amount should be a positive value. Use Heal method to heal.");
            return;
        }

        currentHealth -= damageAmount; // Reduce current health by the damage amount

        // Check if health has dropped below 0 (dead)
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(); // Call the Die function if the object's health is less than or equal to 0
        }

        OnHealthChange?.Invoke(currentHealth, maxHealth); // Trigger health change event
    }

    /// <summary>
    /// Function to apply healing to the object's health.
    /// </summary>
    /// <param name="healAmount">The amount of healing to apply.</param>
    public void Heal(int healAmount)
    {
        if (healAmount <= 0)
        {
            Debug.LogWarning("Heal amount should be a positive value. Use TakeDamage method to take damage.");
            return;
        }

        currentHealth += healAmount; // Increase current health by the heal amount

        // Make sure currentHealth doesn't exceed maxHealth
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChange?.Invoke(currentHealth, maxHealth); // Trigger health change event
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        OnHealthChange?.Invoke(currentHealth, maxHealth); // Trigger health change event
    }
}
