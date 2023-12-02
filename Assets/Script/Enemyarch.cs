using Pathfinding;
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
    public float nextwaipointdistance = 3f;
    private Path path;
    private int currentWaipoint = 0;
    bool reachedendofpath = false;
    Seeker seeker;
    private void Awake()
    {
        
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindGameObjectWithTag("door");
        seeker = GetComponent<Seeker>();
        door = GameObject.FindGameObjectWithTag("door");
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= viewRange && canShoot)
        {
            StartCoroutine(ShootBullet());
        }

        if (path == null)
        {
            // do nothing
        }
        if (currentWaipoint >= path.vectorPath.Count)
        {
            reachedendofpath = true;
        }
        else
        {
            reachedendofpath = false;
        }
        if (health <= 0)
        {
            if (door != null)
            {
                door.GetComponent<Door>().OnEnemyDestroyed();
            }
            Destroy(gameObject);
        }
    }

    private void OnpathComplete(Path P)
    {
        if (!P.error)
        {
            path = P;
            currentWaipoint = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        Vector3 movementDirection = directionToPlayer.normalized;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaipoint] - rb.position).normalized;
        Vector2 force = direction * movementSpeed * Time.deltaTime;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);

        if (distance < nextwaipointdistance)
        {
            currentWaipoint++;
        }

        rb.AddForce(force);

        
        if (distanceToPlayer <= viewRange)
        {
            chasing = true;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.transform.position, OnpathComplete);
        }
    }

    protected override void Shoot()
    {

    }

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