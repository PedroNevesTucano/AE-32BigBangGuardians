using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    public GameObject EnemyBulletPrefab;
    public float movementSpeed;
    public float bulletSpeed = 30f;
    public float bulletCooldown;
    public float bulletCooldownBase;
    public float viewRange;
    public float health;
    public bool chasing;

    private Rigidbody2D rb;
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

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
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
        void Shoot()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletTag"))
        {
            health -= 15;
        }
        if (collision.CompareTag("BigBulletTag"))
        {
            health -= 30;
        }
    }
}