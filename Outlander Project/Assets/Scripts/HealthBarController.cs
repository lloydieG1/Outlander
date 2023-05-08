using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Health healthTarget;

    private Slider healthSlider;

    void Start()
    {
        healthSlider = GetComponent<Slider>();

        if (healthTarget != null)
        {
            healthSlider.maxValue = healthTarget.MaxHealth;
            healthSlider.value = healthTarget.CurrentHealth;
        }
        else
        {
            Debug.LogError("Health target is not assigned.");
        }
    }

    void Update()
    {
        if (healthTarget != null)
        {
            healthSlider.value = healthTarget.CurrentHealth;
        }
    }
}
