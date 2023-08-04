using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public delegate void HealthChangeEvent(float currentHealth, float maxHealth); // Delegate to handle health change event
    public static event HealthChangeEvent OnHealthChange; // Event triggered when health changes

    [SerializeField]
    private float maxHealth = 100;

    [SerializeField]
    [Tooltip("The percentage of health with which an object is created.")]
    [Range(0f, 100f)]
    private float startHealthPercent = 100;

    [SerializeField]
    [Tooltip("Multiplier applied to damage taken.")]
    private float damageMultiplier = 1;

    [SerializeField]
    [Tooltip("Multiplier applied to healing received.")]
    private float healMultiplier = 1;

    private float currentHealth; // Current health value for the object

    public float CurrentHealth => currentHealth; // Public property for accessing the current health value
    public float MaxHealth => maxHealth; // Public property for accessing the maximum health value

    private void Start()
    {
        currentHealth = Mathf.RoundToInt(maxHealth * (startHealthPercent / 100f));
    }

    /// <summary>
    /// Function to apply damage to the object's health.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to apply.</param>
    public void TakeDamage(float damageAmount)
    {
        if (damageAmount <= 0)
        {
            Debug.LogWarning("Damage amount should be a positive value. Use Heal method to heal.");
            return;
        }

        currentHealth -= damageAmount * damageMultiplier; // Reduce current health by the damage amount

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
    public void Heal(float healAmount)
    {
        if (healAmount <= 0)
        {
            Debug.LogWarning("Heal amount should be a positive value. Use TakeDamage method to take damage.");
            return;
        }

        currentHealth += healAmount * healMultiplier; // Increase current health by the heal amount

        // Make sure currentHealth doesn't exceed maxHealth
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChange?.Invoke(currentHealth, maxHealth); // Trigger health change event
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        OnHealthChange?.Invoke(currentHealth, maxHealth); // Trigger health change event
    }
    
    /// <summary>
    /// Function to apply regeneration over time to the object's health.
    /// </summary>
    /// <param name="regenAmount">The amount of regeneration to apply.</param>
    /// <param name="regenDuration">The duration of the regeneration process.</param>
    public void ApplyRegeneration(float regenAmount, float regenDuration)
    {
        StartCoroutine(RegenerationCoroutine(regenAmount, regenDuration));
    }

    private IEnumerator RegenerationCoroutine(float regenAmount, float regenDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < regenDuration)
        {
            // Calculate the amount of health to regenerate during this frame
            float healthToRegen = Mathf.RoundToInt(regenAmount * healMultiplier * Time.deltaTime);

            // Increase current health by the health to regenerate
            currentHealth = Mathf.Min(currentHealth + healthToRegen, maxHealth);

            OnHealthChange?.Invoke(currentHealth, maxHealth); // Trigger health change event

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }

    /// <summary>
    /// Function to consume a potion and apply instant healing.
    /// </summary>
    /// <param name="potionHealAmount">The amount of healing the potion provides.</param>
    public void ConsumePotion(int potionHealAmount)
    {
        int totalHealAmount = Mathf.RoundToInt(potionHealAmount * healMultiplier);
        currentHealth = Mathf.Min(currentHealth + totalHealAmount, maxHealth);

        OnHealthChange?.Invoke(currentHealth, maxHealth); // Trigger health change event
    }
}
