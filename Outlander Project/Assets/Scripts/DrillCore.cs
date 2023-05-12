using UnityEngine;

public class DrillCore : MonoBehaviour
{
    private Health health;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (health != null && health.CurrentHealth <= 0)
        {
            PlayerLose.Instance.SelfDestructAndLoadMainMenu();
        }
    }
}
