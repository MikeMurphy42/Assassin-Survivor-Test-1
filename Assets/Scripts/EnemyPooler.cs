using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPooler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;
    public float spawnInterval = 1.0f;
    public float initialDelay = 5.0f; // new variable to set initial delay
    public Transform minSpawn;
    public Transform maxSpawn;
    public float maxDistanceFromPlayer = 20f;
    public float maxDistanceCheckInterval = 0.5f;
    public int maxActiveEnemies = 5;

    private List<GameObject> pooledEnemies = new List<GameObject>();
    private float spawnTimer = 0.0f;
    private Transform target;
    private float maxDistanceCheckTimer = 0f;
    public int activeEnemyCount = 0;

    void Start()
    {
        target = PlayerHealthController.instance.transform;

        if (minSpawn == null)
        {
            minSpawn = transform.Find("MinSpawn");
        }

        if (maxSpawn == null)
        {
            maxSpawn = transform.Find("MaxSpawn");
        }

        StartCoroutine(SpawnEnemies()); // start the coroutine to spawn enemies
    }

    void Update()
    {
        // Update the positions of the minSpawn and maxSpawn to follow the player
        if (target != null)
        {
            minSpawn.position = target.position + new Vector3(-11f, -7f, 0f);
            maxSpawn.position = target.position + new Vector3(11f, 7f, 0f);
            transform.position = target.position;
        }

        maxDistanceCheckTimer += Time.deltaTime;

        // Check the distance between enemies and the player a certain number of times per second
        if (maxDistanceCheckTimer >= maxDistanceCheckInterval)
        {
            maxDistanceCheckTimer -= maxDistanceCheckInterval;

            // Deactivate enemies that are too far from the player
            foreach (GameObject enemy in pooledEnemies)
            {
                if (enemy.activeSelf && Vector3.Distance(enemy.transform.position, target.position) > maxDistanceFromPlayer)
                {
                    enemy.SetActive(false);
                    activeEnemyCount--;
                }
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialDelay); // wait for initial delay before starting to spawn enemies

        // Create initial pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, SelectSpawnPoint(), transform.rotation);
            enemy.SetActive(false);
            pooledEnemies.Add(enemy);
        }

        while (true)
        {
            spawnTimer += Time.deltaTime;

            // If it's time to spawn a new enemy, get one from the pool and activate it
            if (spawnTimer >= spawnInterval && activeEnemyCount < maxActiveEnemies)
            {
                spawnTimer -= spawnInterval;

                GameObject enemy = GetEnemy();
                enemy.SetActive(true);
                activeEnemyCount++;
            }

            yield return null;
        }
    }

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

    // Get an enemy from the pool
    public GameObject GetEnemy()
    {
        foreach (GameObject enemy in pooledEnemies)
        {
            if (!enemy.activeSelf)
            {
                enemy.transform.position = SelectSpawnPoint();
                enemy.SetActive(true);
                return enemy;
            }
        }

        // If all enemies are active, create a new one
        GameObject newEnemy = Instantiate(enemyPrefab, SelectSpawnPoint(), transform.rotation);
        pooledEnemies.Add(newEnemy);
        return newEnemy;
    }
    public void DisableEnemy()
    {
        activeEnemyCount--;
    }
   


}