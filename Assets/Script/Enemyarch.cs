using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemyarch : AbstractEnemy
{
    private float movementSpeed = 250;
    private bool chasing;
    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 1.5f;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
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

        if (bulletCooldown < 0.2f)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0f, 0f, 1f);
        }

        if (chasing || health <= 29)
        {
            if (distanceToPlayer >= 3)
            {
                Shoot();
                rb.velocity = movementSpeed * Time.fixedDeltaTime * movementDirection;
            }
            else if (distanceToPlayer < 2f)
            {
                Shoot();
                rb.velocity = movementSpeed * Time.fixedDeltaTime * -movementDirection;
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

    protected override void Shoot()
    {
        if (CooldownChecker())
        {
            int rotation = 10;
            int angles = 0;

            for(int i = 0; i < 3; i++)
            {

                Quaternion rotationDegrees = Quaternion.Euler(0, 0, angles);
                Vector2 direction = (player.transform.position - transform.position).normalized;
                Vector2 actualdirection = rotationDegrees * direction;

                float angle = Mathf.Atan2(direction.y, direction.x);
                float angleDegrees = angle * Mathf.Rad2Deg;

                GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = bulletSpeed * Time.fixedDeltaTime * actualdirection;
                angles += rotation;
                angleDegrees -= rotation;
            }
        }
    }
}