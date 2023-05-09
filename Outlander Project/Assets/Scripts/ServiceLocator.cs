using UnityEngine;
using System.Collections.Generic;

public class ServiceLocator : MonoBehaviour
{
    private Dictionary<System.Type, Component> services = new Dictionary<System.Type, Component>();

    public static ServiceLocator Instance { get; private set; }

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
            return;
        }
    }

    public void RegisterService<T>(T service) where T : Component
    {
        services[typeof(T)] = service;
    }

    public T GetService<T>() where T : Component
    {
        if (services.TryGetValue(typeof(T), out Component service))
        {
            return (T)service;
        }
        return null;
    }
}
