using UnityEngine;

public class ResourceTrigger : MonoBehaviour
{
    public enum ResourceType { Fuel, Ammo, Gold }

    [SerializeField] private ResourceType resourceType;
    [SerializeField] private float resourceAmountPerSecond;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spaceship"))
        {
            ServiceLocator.Instance.GetService<AudioManager>().Play("FuelSuck");
            InvokeRepeating(nameof(GiveResources), 0f, 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spaceship"))
        {
            ServiceLocator.Instance.GetService<AudioManager>().Stop("FuelSuck");
            CancelInvoke(nameof(GiveResources));
        }
    }

    private void GiveResources()
    {
        ResourceManager resourceManager = ResourceManager.Instance;
        switch (resourceType)
        {
            case ResourceType.Fuel:
                
                resourceManager.AddFuel(resourceAmountPerSecond/10f);
                break;
            case ResourceType.Ammo:
                //ServiceLocator.Instance.GetService<AudioManager>().Play("AmmoSuck");
                resourceManager.AddAmmo(resourceAmountPerSecond/10f);
                break;
            case ResourceType.Gold:
                resourceManager.AddGold(Mathf.RoundToInt(resourceAmountPerSecond/10f));
                break;
        }
    }
}
