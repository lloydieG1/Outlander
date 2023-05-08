using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillCore : MonoBehaviour
{
    public int initialHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
