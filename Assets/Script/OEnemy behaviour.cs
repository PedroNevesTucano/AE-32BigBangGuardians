using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class OEnemybehaviour : MonoBehaviour
{
    public Transform player;
    public float viewRange;
    public bool chasing;
    private Rigidbody2D rb;
    public float health;
    public float movementSpeed;
    public float bulletCooldown;
    public float bulletCooldownBase;
    public float bulletSpeed = 30f;
    public GameObject EnemyBulletPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 1.5f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        Vector3 movementDirection = directionToPlayer.normalized;

        if (bulletCooldown > 0 && chasing)
        {
            bulletCooldown -= Time.fixedDeltaTime;
        }
        if (distanceToPlayer <= viewRange)
        {
            chasing = true;
        }

        if (chasing || health <= 29)
        {
            if (distanceToPlayer >= 3)
            {
                Shoot();
                rb.velocity = movementDirection * movementSpeed * Time.fixedDeltaTime;
            }
            else if (distanceToPlayer < 2f)
            {
                Shoot();
                rb.velocity = -movementDirection * movementSpeed * Time.fixedDeltaTime;
            }
            else
            {
                Shoot();
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void Shoot()
    {
        if (0 < bulletCooldown)
        {
            return;
        }
        bulletCooldown = bulletCooldownBase;

        Vector3 direction = (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
    }
}
