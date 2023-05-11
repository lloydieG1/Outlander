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
            ServiceLocator.Instance.GetService<AudioManager>().Play("Loss");

            //lose after a delay
            Invoke("Lose", 2f);
            // disable sprite
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Lose()
    {
        Debug.Log("DrillCore.Lose()");
        LevelLoader.Instance.LoadLevel(0);
    }
}
