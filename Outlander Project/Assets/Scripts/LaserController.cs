using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserSpawnPoint;
    [SerializeField] private float laserLength;
    [SerializeField] private LayerMask laserHitLayers;
    [SerializeField] private int laserAmmoConsumptionRate;
    [SerializeField] private float damagePerSecond;

    private GameObject laserInstance;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = laserPrefab.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ResourceManager.Instance.GetAmmo() > 0)
        {
            // Instantiate the laser at the spawn point and make it a child of the laser spawn point
            laserInstance = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation, laserSpawnPoint);

            
            // Get the laser's sprite renderer and enable it
            spriteRenderer = laserInstance.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = true;
            // Consume ammo
            ResourceManager.Instance.ConsumeAmmo(laserAmmoConsumptionRate * Time.deltaTime);
        }

        if (Input.GetButton("Fire1"))
        {
            if (laserInstance != null && ResourceManager.Instance.GetAmmo() > 0)
            {
                FireLaser();
                ResourceManager.Instance.ConsumeAmmo(laserAmmoConsumptionRate * Time.deltaTime);
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            // Destroy the laser instance when the fire button is released
            if (laserInstance != null)
            {
                Destroy(laserInstance);
            }
        }
    }

    private void FireLaser()
    {
        // Cast a ray from the laser spawn point in the direction it is facing
        RaycastHit2D hit = Physics2D.Raycast(laserSpawnPoint.position, laserSpawnPoint.up, laserLength, laserHitLayers);

        Debug.DrawRay(laserSpawnPoint.position, laserSpawnPoint.up * laserLength, Color.red);
        if (hit.collider != null)
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
            // Apply damage to the hit object if it has a Health component
            Health hitHealth = hit.collider.GetComponent<Health>();
            if (hitHealth != null)
            {
                Debug.Log("Hit health not null");
                hitHealth.TakeDamage((damagePerSecond * Time.deltaTime));
            }
            // Set the scale of the laser based on the distance to the hit object
            laserInstance.transform.localScale = new Vector2(1, hit.distance);
        }
        else
        {
            // Set the scale of the laser back to its original length
            laserInstance.transform.localScale = new Vector2(1, laserLength);
        }
    }

}