using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroundPrefab
{
    public GameObject prefab;
    public Vector2 offset;
}

public class GroundManager : MonoBehaviour
{
    [SerializeField] List<GroundPrefab> groundPrefabs; // Assign your Ground Prefabs and their offsets in Inspector
    [SerializeField] GameObject player;               // Assign your Player in Inspector
    [SerializeField] int poolSizeX = 5;               // Number of ground objects in pool in X direction
    [SerializeField] int poolSizeY = 5;               // Number of ground objects in pool in Y direction

    private List<List<GameObject>> groundPool;  // 2D List to hold the ground objects
    private float spawnXPos = 0;               // Position to spawn the next ground object in X
    private float spawnYPos = 0;               // Position to spawn the next ground object in Y

    private void Start()
    {
        if (groundPrefabs.Count == 0)
        {
            Debug.LogError("No ground prefabs assigned.");
            return;
        }

        groundPool = new List<List<GameObject>>();

        for (int i = 0; i < poolSizeY; i++)
        {
            List<GameObject> groundRow = new List<GameObject>();
            for (int j = 0; j < poolSizeX; j++)
            {
                GroundPrefab groundPrefab = GetRandomGroundPrefab();
                GameObject ground = Instantiate(groundPrefab.prefab, new Vector3(spawnXPos + groundPrefab.offset.x, spawnYPos + groundPrefab.offset.y, 0), Quaternion.identity);
                spawnXPos += groundPrefab.prefab.transform.localScale.x;
                groundRow.Add(ground);
            }
            groundPool.Add(groundRow);
            spawnXPos = 0;
            spawnYPos += groundPrefabs[0].prefab.transform.localScale.y;
        }
    }

    private void Update()
    {
        // Check X direction
        // If the player has passed the halfway point of the first ground object in the pool
        if (Mathf.Abs(player.transform.position.x - groundPool[0][0].transform.position.x) > groundPool[0][0].transform.localScale.x / 2)
        {
            if (player.transform.position.x > groundPool[0][0].transform.position.x)
            {
                MoveFirstColumnToEnd();
            }
            else
            {
                MoveLastColumnToStart();
            }
        }

        // Check Y direction
        // If the player has passed the halfway point of the first ground object in the pool
        if (Mathf.Abs(player.transform.position.y - groundPool[0][0].transform.position.y) > groundPool[0][0].transform.localScale.y / 2)
        {
            if (player.transform.position.y > groundPool[0][0].transform.position.y)
            {
                MoveFirstRowToEnd();
            }
            else
            {
                MoveLastRowToStart();
            }
        }
    }

    private GroundPrefab GetRandomGroundPrefab()
    {
        int randomIndex = Random.Range(0, groundPrefabs.Count);
        return groundPrefabs[randomIndex];
    }

    private void MoveFirstColumnToEnd()
    {
        for (int i = 0; i < poolSizeY; i++)
        {
            GameObject ground = groundPool[i][0];
            groundPool[i].RemoveAt(0);
            ground.transform.position = new Vector3(groundPool[i][poolSizeX - 1].transform.position.x + ground.transform.localScale.x, ground.transform.position.y, 0);
            groundPool[i].Add(ground);
        }
    }

    private void MoveLastColumnToStart()
    {
        for (int i = 0; i < poolSizeY; i++)
        {
            GameObject ground = groundPool[i][poolSizeX - 1];
            groundPool[i].RemoveAt(poolSizeX - 1);
            ground.transform.position = new Vector3(groundPool[i][0].transform.position.x - ground.transform.localScale.x, ground.transform.position.y, 0);
            groundPool[i].Insert(0, ground);
        }
    }

    private void MoveFirstRowToEnd()
    {
        List<GameObject> groundRow = groundPool[0];
        groundPool.RemoveAt(0);
        for (int i = 0; i < poolSizeX; i++)
        {
            GameObject ground = groundRow[i];
            ground.transform.position = new Vector3(ground.transform.position.x, groundPool[poolSizeY - 1][0].transform.position.y + ground.transform.localScale.y, 0);
        }
        groundPool.Add(groundRow);
    }

    private void MoveLastRowToStart()
    {
        List<GameObject> groundRow = groundPool[poolSizeY - 1];
        groundPool.RemoveAt(poolSizeY - 1);
        for (int i = 0; i < poolSizeX; i++)
        {
            GameObject ground = groundRow[i];
            ground.transform.position = new Vector3(ground.transform.position.x, groundPool[0][0].transform.position.y - ground.transform.localScale.y, 0);
        }
        groundPool.Insert(0, groundRow);
    }
}

