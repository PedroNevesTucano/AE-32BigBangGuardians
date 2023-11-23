using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
 
public class OEnemybehaviour : AbstractEnemy
{
    public bool chasing;
    public float movementSpeed;
    public Transform[] teleportPositions;
    private int currentPositionIndex = 0;
    private float timer = 2;



    // Start is called before the first frame update
    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 1.5f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        Debug.Log(distanceToPlayer);


        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.fixedDeltaTime;
        }

        if (distanceToPlayer <= 10)
        {
            Shoot();
        }

        
    }

    void Update()
    {
        if (timer >= 2)
        {
            TeleportToNextPosition();
            timer = 0;
        }
        timer += Time.deltaTime;
        if (currentPositionIndex == teleportPositions.Length)
        {
            currentPositionIndex = 0;
        }
    }

    void TeleportToNextPosition()
    {
        if (teleportPositions.Length == 0)
        {
            Debug.LogError("No teleport positions assigned.");
            return;
        }

        transform.position = teleportPositions[currentPositionIndex].position;
        currentPositionIndex = currentPositionIndex + 1;

    }


    protected override void Shoot()
    {
        if (0 < bulletCooldown)
        {
            return;
        }
        bulletCooldown = bulletCooldownBase;

        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
    }
}
