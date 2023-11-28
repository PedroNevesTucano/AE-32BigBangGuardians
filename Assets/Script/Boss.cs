using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Boss : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    public GameObject player;
    public GameObject enemyspawnerprefab;
    public float timer;
    public float timeToChangePattern = 10f;
    public GameObject enemybulletprefab;
    public int iterations;
    public int spawnRadius = 5;
    public float spawnInterval = 2f;
    public int bulletSpeed;
    public int health = 30;
    public bool istalking = false;

    private void Update()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < 3 && Input.GetKeyDown(KeyCode.E))
        {
            dialogueSystem.StartDialogue();
            istalking = true;
        }
        if(dialogueSystem.timer >= 3) 
        {
            dialogueOutcome();
        }

        if(health <= 0) 
        {
            Destroy(gameObject);
        }

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (istalking == true && collision.CompareTag("BulletTag"))
        {
            health -= 15;
        }
        if (istalking == true && collision.CompareTag("BigBulletTag"))
        {
            health -= 30;
        }
    }

    private void Start()
    {
        
    }

    private void dialogueOutcome()
    {
        switch (dialogueSystem.currentNode.Finaloutcome)
        {
            case "Good":
                //make so that it has a happy ending
                break;
            case "Bad":
                Battle();
                break;
        }
    }

    private void Battle() 
    {
        timer += Time.deltaTime;


        if (timer >= timeToChangePattern)
        {
            ChangeAttackPattern();
            timer = 0f; // Reset the timer
        }
    }

    void ChangeAttackPattern()
    {
        int randomPattern = Random.Range(1, 3);
        Debug.Log(randomPattern);
        switch (randomPattern)
        {
            case 1:
                StartCoroutine(Shoot());
                break;
            case 2:
                StartCoroutine(SpawnSimpleEnemies());
                break;
                // Add more cases for additional attack patterns
        }
    }

    private IEnumerator Shoot()
    {
        float timerbetweenshoots = 0.1f;
        float angleoffset = 0;

        for (int i = 0; i < iterations; i++)
        {
            GameObject bullet = Instantiate(enemybulletprefab, transform.position, Quaternion.Euler(0, 0, 0 + angleoffset));
            GameObject bullet2 = Instantiate(enemybulletprefab, transform.position, Quaternion.Euler(0, 0, 90 + angleoffset));
            GameObject bullet3 = Instantiate(enemybulletprefab, transform.position, Quaternion.Euler(0, 0, 180 + angleoffset));
            GameObject bullet4 = Instantiate(enemybulletprefab, transform.position, Quaternion.Euler(0, 0, 270 + angleoffset));

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRb2 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRb3 = bullet3.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRb4 = bullet4.GetComponent<Rigidbody2D>();

            Quaternion rotationoffset = Quaternion.Euler(0, 0, angleoffset);

            bulletRb.velocity = rotationoffset * Vector2.right * bulletSpeed * Time.fixedDeltaTime;
            bulletRb2.velocity = rotationoffset * Vector2.up * bulletSpeed * Time.fixedDeltaTime;
            bulletRb3.velocity = rotationoffset * Vector2.left * bulletSpeed * Time.fixedDeltaTime;
            bulletRb4.velocity = rotationoffset * Vector2.down * bulletSpeed * Time.fixedDeltaTime;

            angleoffset += 5;
            yield return new WaitForSeconds(timerbetweenshoots);
        }
    }

    private IEnumerator SpawnSimpleEnemies() 
    {
        for (int i = 0; i < 3; i++)
        {
            // Calculate a random position within the spawn radius
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Spawn the enemy at the random position
            Instantiate(enemyspawnerprefab, randomPosition, Quaternion.identity);

            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}