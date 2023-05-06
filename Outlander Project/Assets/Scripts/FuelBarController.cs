using UnityEngine;
using UnityEngine.UI;

public class FuelBarController : MonoBehaviour
{
    private ResourceManager resourceManager;
    private Slider fuelSlider;

    void Start()
    {
        resourceManager = ResourceManager.Instance;
        fuelSlider = GetComponent<Slider>();
        fuelSlider.maxValue = resourceManager.GetMaxFuel();
    }

    void Update()
    {
        fuelSlider.value = resourceManager.GetFuel();
    }
}
