using UnityEngine;

public class SwarmSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private float baseSpawnRate;
    [SerializeField] private float initialForce = 1.0f; // New field for initial force

    private float nextSpawnTime;
    private int currentWave = 0;
    private bool canSpawn = false;

    void Update()
{
    if (canSpawn && Time.time >= nextSpawnTime)
    {
        SpawnEnemy();
        // Spawn rate increases with the wave number
        float spawnRate = baseSpawnRate * WaveManager.Instance.CurrentWave; 
        Debug.Log("CURRENT WAVE: " + WaveManager.Instance.CurrentWave);
        Debug.Log("SwarmSpawner: Update: spawnRate = " + spawnRate);
        nextSpawnTime = Time.time + 1f / spawnRate; 
    }
}

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Swarmlet enemyScript = newEnemy.GetComponent<Swarmlet>();
        enemyScript.SetTarget(target);

        // Add initial velocity in a random direction
        Rigidbody2D rb = newEnemy.GetComponent<Rigidbody2D>();
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDirection * initialForce, ForceMode2D.Impulse); // Use AddForce with initialForce and ForceMode2D.Impulse
        Debug.Log("SwarmSpawner: SpawnEnemy: randomDirection = " + randomDirection);
    }

    float GetSpawnRateForWave()
    {
        // Modify this function to return the spawn rate based on the current wave
        return baseSpawnRate / (currentWave + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpawnTrigger"))
        {
            Debug.Log("SwarmSpawner: OnTriggerEnter2D");
            canSpawn = true;
            GameEvents.Instance.IncreaseWave();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpawnTrigger"))
        {
            canSpawn = false;
        }
    }
}
