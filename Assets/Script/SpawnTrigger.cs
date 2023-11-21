using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject spawnAreaPrefab;

    private float spawnCooldown = 0.1f;
    private bool triggered;

    public Vector3 spawnPosition;
    public Vector3 spawnPosition2;
    public Vector3 spawnPosition3;

    private bool spawnAreaOneInstantiated = false;
    private bool spawnAreaTwoInstantiated = false;
    private bool spawnAreaThreeInstantiated = false;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (spawnCooldown > 0)
        {
            spawnCooldown -= Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (triggered == true)
        {
            if (spawnPosition != Vector3.zero && spawnAreaOneInstantiated == false)
            {
                GameObject spawnArea = Instantiate(spawnAreaPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
                spawnCooldown = Random.Range(0.1f, 1f);
                spawnAreaOneInstantiated = true;
            }
            if (spawnCooldown <= 0 && spawnPosition2 != Vector3.zero && spawnAreaTwoInstantiated == false)
            {
                GameObject spawnArea2 = Instantiate(spawnAreaPrefab, spawnPosition2, Quaternion.Euler(0, 0, 0));
                spawnCooldown = Random.Range(0.1f, 1f);
                spawnAreaTwoInstantiated = true;
            }
            if (spawnCooldown <= 0 && spawnPosition3 != Vector3.zero && spawnAreaThreeInstantiated == false)
            {
                GameObject spawnArea3 = Instantiate(spawnAreaPrefab, spawnPosition3, Quaternion.Euler(0, 0, 0));
                spawnAreaThreeInstantiated = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
        }
    }
}
