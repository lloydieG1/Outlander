using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // resource controller is a singleton
    public static ResourceManager Instance { get; private set; }
    [SerializeField] private float fuel;
    [SerializeField] private int gold;
    [SerializeField] private float maxFuel;
    [SerializeField] private int maxGold;

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

    public float GetFuel()
    {
        return fuel;
    }

    public void AddFuel(float amount)
    {
        fuel += amount;
    }

    public void ConsumeFuel(float amount)
    {
        fuel = Mathf.Max(fuel - amount, 0);
    }

    public int GetGold()
    {
        return gold;
    }

    public float GetMaxFuel()
    {
        return maxFuel;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void SpendGold(int amount)
    {
        gold = Mathf.Max(gold - amount, 0);
    }
}
