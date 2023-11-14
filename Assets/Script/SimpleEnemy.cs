using UnityEngine;
public class SimpleEnemy : AbstractEnemy
{
    public float movementSpeed;
    public bool chasing;
    private protected override void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 1.5f;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private protected override void FixedUpdate()
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
    
    private protected override void Shoot()
    {
        if (bulletCooldown > 0)
        {
            return;
        }
        bulletCooldown = bulletCooldownBase;

        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;
        
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bulletSpeed * Time.fixedDeltaTime * direction;
    }

    new public void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}