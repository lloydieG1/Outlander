using UnityEngine;

public class SwarmSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private float spawnRate;

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Swarmlet enemyScript = newEnemy.GetComponent<Swarmlet>();
        enemyScript.SetTarget(target);
    }
}