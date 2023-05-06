using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    public GameObject SpaceShip;


    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = ResourceManager.Instance.GetFuel();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = ResourceManager.Instance.GetFuel();
    }
}
