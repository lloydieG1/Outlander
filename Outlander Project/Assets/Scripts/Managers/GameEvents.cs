using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    public event Action OnPlayerLose;
    public event Action OnIncreaseWave;

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

    public void PlayerLose()
    {
        OnPlayerLose?.Invoke();
    }

    public void IncreaseWave()
    {
        OnIncreaseWave?.Invoke();
    }
}
