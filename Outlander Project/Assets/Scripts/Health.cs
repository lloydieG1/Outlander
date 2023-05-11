using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float initialHealth = 100f;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float InitialHealth
    {
        get => initialHealth;
        set => initialHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(value, 0);
    }

    public float CurrentHealth
    {
        get => currentHealth;
        private set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    void Start()
    {
        CurrentHealth = InitialHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0 && gameObject.CompareTag("Swarmlet"))
        {
            Destroy(gameObject);
        }
    }

    // Add this method to handle continuous damage from the laser
    public void TakeDamagePerSecond(float damage)
    {
        CurrentHealth -= (int)damage;

        if (CurrentHealth <= 0 && gameObject.CompareTag("Swarmlet"))
        {
            Destroy(gameObject);
        }
    }
}
