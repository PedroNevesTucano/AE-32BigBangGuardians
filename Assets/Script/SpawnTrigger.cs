using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public Vector2[] spawnPoints; // Array of Vector2 spawn points for this enemy type
    [HideInInspector]
    public bool isSpawned; // Flag to track if enemies for this type have been spawned
}

public class SpawnTrigger : MonoBehaviour
{
    public GameObject Door;

    public bool triggered;

    private bool doorspawned = false;

    public Vector2 doorspawnposition;

    public EnemySpawnInfo[] enemiesToSpawn; // Array of enemy prefabs and their counts



    private void Update()
    {
        if (triggered == true && doorspawned == false)
        {
            Instantiate(Door, doorspawnposition, Quaternion.identity);
            SpawnEnemies();
            doorspawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    void SpawnEnemies()
    {
            foreach (var enemyInfo in enemiesToSpawn)
            {
                if (!enemyInfo.isSpawned)
                {
                    foreach (Vector2 spawnPoint in enemyInfo.spawnPoints)
                    {
                        Instantiate(enemyInfo.enemyPrefab, spawnPoint, Quaternion.identity);
                    }
                    // Set the flag to indicate that enemies for this type have been spawned
                    enemyInfo.isSpawned = true;
                }
            }
    }
}
