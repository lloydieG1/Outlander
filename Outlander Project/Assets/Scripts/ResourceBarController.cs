using UnityEngine;
using UnityEngine.UI;

public class ResourceBarController : MonoBehaviour
{
    public enum ResourceType { Fuel, Ammo, Gold }

    [SerializeField] private ResourceType resourceType;

    private ResourceManager resourceManager;
    private Slider resourceSlider;

    void Start()
    {
        resourceManager = ResourceManager.Instance;
        resourceSlider = GetComponent<Slider>();

        switch (resourceType)
        {
            case ResourceType.Fuel:
                resourceSlider.maxValue = resourceManager.GetMaxFuel();
                break;
            case ResourceType.Ammo:
                resourceSlider.maxValue = resourceManager.GetMaxAmmo();
                break;
            case ResourceType.Gold:
                resourceSlider.maxValue = resourceManager.GetMaxGold();
                break;
        }
    }

    void Update()
    {
        switch (resourceType)
        {
            case ResourceType.Fuel:
                resourceSlider.value = resourceManager.GetFuel();
                break;
            case ResourceType.Ammo:
                resourceSlider.value = resourceManager.GetAmmo();
                break;
            case ResourceType.Gold:
                resourceSlider.value = resourceManager.GetGold();
                break;
        }
    }
}
