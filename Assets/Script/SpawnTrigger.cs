using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject spawnAreaPrefab;

    public Vector3 spawnPosition;
    public Vector3 spawnPosition2;
    public Vector3 spawnPosition3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (spawnPosition != Vector3.zero)
            {
                GameObject spawnArea = Instantiate(spawnAreaPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
            }
            if (spawnPosition2 != Vector3.zero)
            {
                GameObject spawnArea2 = Instantiate(spawnAreaPrefab, spawnPosition2, Quaternion.Euler(0, 0, 0));
            }
            if (spawnPosition3 != Vector3.zero)
            {
                GameObject spawnArea3 = Instantiate(spawnAreaPrefab, spawnPosition3, Quaternion.Euler(0, 0, 0));
            }
            Destroy(gameObject);
        }
    }
}
