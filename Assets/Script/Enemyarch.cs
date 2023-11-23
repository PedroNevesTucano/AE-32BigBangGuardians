using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;

public class Enemyarch : AbstractEnemy
{
    private float movementSpeed = 250;
    private bool chasing;
    private float shootInterval = 0.1f;
    private bool canShoot = true;
    public bool firstcycle = true;
    private float iterations = 19;
    private void Awake()
    {
        
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= viewRange && canShoot)
        {
            StartCoroutine(ShootBullet());
        }
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        Vector3 movementDirection = directionToPlayer.normalized;

        if (bulletCooldown > 0 && chasing)
        {
            //bulletCooldown -= Time.fixedDeltaTime;
        }
        if (distanceToPlayer <= viewRange)
        {
            chasing = true;
        }

        if (chasing || health <= 29)
        {
            if (distanceToPlayer >= 3)
            {
                rb.velocity = movementSpeed * Time.fixedDeltaTime * movementDirection;
            }
            else if (distanceToPlayer < 2f)
            {
                rb.velocity = movementSpeed * Time.fixedDeltaTime * -movementDirection;
            }
            else
            {        
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

    }

    /*protected override void Shoot()
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
                angleDegrees += rotation;
            }
        }
    }*/

    private IEnumerator ShootBullet()
    {
        canShoot = false;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;
        float angleoffset = 20;


        float actualangle = 1;
        for (int i = 0; i < iterations; i++)
        {
            Quaternion rotationMinus10Degrees = Quaternion.Euler(0, 0, actualangle);
            Vector2 actualdirection = rotationMinus10Degrees * direction.normalized;

            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = actualdirection.normalized * bulletSpeed * Time.fixedDeltaTime;

            // Wait for the specified interval before shooting the next bullet
            yield return new WaitForSeconds(shootInterval);

            // Increment the angle for the next bullet
            if (firstcycle == true)
            {
                actualangle += angleoffset;
                angleDegrees += angleoffset;
            }
            else
            {
                actualangle += angleoffset * -1;
                angleDegrees += angleoffset * -1;
            }
        }

        // Cooldown before the next cycle
        yield return new WaitForSeconds(bulletCooldown);
        canShoot = true;
        
        if (firstcycle == true)
        {
            firstcycle = false;
        }
        else
        {
            firstcycle = true;
        }
    }
}