using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;
    public float spawnInterval = 1.0f;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private float spawnTimer = 0.0f;
    
    
    public Transform minSpawn, maxSpawn;

    private Transform target;
    

    // Start is called before the first frame update
    void Start()
    {
        // Create initial pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, SelectSpawnPoint(), transform.rotation);
            enemy.SetActive(false);
            pool.Enqueue(enemy);
        }

        target = PlayerHealthController.instance.transform;

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        // If it's time to spawn a new enemy, get one from the pool and activate it
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer -= spawnInterval;

            GameObject enemy = GetEnemy();
            enemy.SetActive(true);
        }
    }
    
    // Trying to Set Spawn Boundaries

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
        
        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        
        return spawnPoint;
    }

    // Trying to Set Spawn Boundaries
    
    
    // Get an enemy from the pool
    public GameObject GetEnemy()
    {
        if (pool.Count == 0)
        {
            // If the pool is empty, create a new enemy
            GameObject enemy = Instantiate(enemyPrefab, SelectSpawnPoint(), transform.rotation);
            pool.Enqueue(enemy);
        }

        GameObject pooledEnemy = pool.Dequeue();
        pooledEnemy.SetActive(true);
        return pooledEnemy;
    }

    // Return an enemy to the pool
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}