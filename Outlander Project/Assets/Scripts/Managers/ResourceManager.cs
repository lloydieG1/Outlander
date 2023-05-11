using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // resource controller is a singleton
    public static ResourceManager Instance { get; private set; }
    [SerializeField] private float fuel;
    [SerializeField] private int gold;
    [SerializeField] private float ammo; 
    [SerializeField] private float maxFuel;
    [SerializeField] private int maxGold;
    [SerializeField] private float maxAmmo; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //reset
    public void Reset()
    {
        fuel = maxFuel;
        gold = maxGold;
        ammo = maxAmmo;
    }

    public float GetFuel()
    {
        return fuel;
    }

    public void AddFuel(float amount)
    {
        fuel = Mathf.Min(fuel + amount, maxFuel);
        
    }

    public void ConsumeFuel(float amount)
    {
        fuel = Mathf.Max(fuel - amount, 0);
    }

    public int GetGold()
    {
        return gold;
    }

    public int GetMaxGold()
    {
        return maxGold;
    }


    public float GetMaxFuel()
    {
        return maxFuel;
    }

    public void AddGold(int amount)
    {
        gold = Mathf.Min(gold + amount, maxGold);
    }

    public void SpendGold(int amount)
    {
        gold = Mathf.Max(gold - amount, 0);
    }

    public float GetAmmo()
    {
        return ammo;
    }

    public float GetMaxAmmo()
    {
        return maxAmmo;
    }

    public void AddAmmo(float amount)
    {
        ammo = Mathf.Min(ammo + amount, maxAmmo);
    }

    public bool ConsumeAmmo(float amount)
    {
        if (ammo >= amount)
        {
            ammo -= amount;
            return true;
        }
        return false;
    }
}
