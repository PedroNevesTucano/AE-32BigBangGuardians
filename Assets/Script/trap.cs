using UnityEngine;
public class trap : AbstractEnemy
{
    public GameObject bigEnemyBulletPrefab;
    public float bigBulletSpeed;
    public float bigBulletCooldown;
    public float bigBulletCooldownBase;

    private protected override void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 0;
        bigBulletCooldownBase = bigBulletCooldown;
        bigBulletCooldown = 0;
    }
    
    private protected override void FixedUpdate()
    {
        if (health > 0)
        {
            if (health >= 20)
            {
                Shoot();
            }
            else if (health <= 19)
            {
                TargetedShoot();
            }
        }
        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.fixedDeltaTime;
        }
        if (bigBulletCooldown > 0)
        {
            bigBulletCooldown -= Time.fixedDeltaTime;
        }
    } 
    private protected override void Shoot()
    {
        if (0 < bulletCooldown)
        {
            return;
        }
        bulletCooldown = bulletCooldownBase;

        Vector3 direction = Vector3.right;

        float angle = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

        rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
    }
    
    void TargetedShoot()
    {
        if (0 < bigBulletCooldown)
        {
            return;
        }
        bigBulletCooldown = bigBulletCooldownBase;
        
        Vector3 direction = (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bigEnemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

        rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bigBulletSpeed * Time.fixedDeltaTime;
    }
    new public void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}