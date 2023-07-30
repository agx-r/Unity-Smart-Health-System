using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMesh healthTextMesh; // Reference to the TextMesh component that will display health

    [SerializeField]
    private Slider healthSlider; // Reference to the Slider component that will display health

    private Health healthComponent; // Reference to the Health component

    private void Awake()
    {
        healthComponent = GetComponent<Health>(); // Get the Health component attached to the same GameObject
    }

    private void OnEnable()
    {
        // Subscribe to the OnHealthChange event of the Health component
        Health.OnHealthChange += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnHealthChange event when this script is disabled or destroyed
        Health.OnHealthChange -= UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        // Update the TextMesh text with the current health value
        healthTextMesh.text = "Health: " + currentHealth.ToString();

        // Update the Slider value based on the current health
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}
