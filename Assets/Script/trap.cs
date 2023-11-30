using UnityEngine;
public class trap : AbstractEnemy
{
    public GameObject bigEnemyBulletPrefab;
    private float bigBulletSpeed = 2000;
    private float bigBulletCooldown = 0.7f;
    //this private field is set to 0 by default
    private float bigBulletCooldownBase;

    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 0;
        bigBulletCooldownBase = bigBulletCooldown;
        bigBulletCooldown = 0;
    }
    
    private void FixedUpdate()
    {
        if (health > 0)
        {
            if (health > 20)
            {
                Shoot();
            }
            else
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

    protected override void Shoot()
    {
        if (CooldownChecker())
        {
            Vector3 direction = Vector3.right;

            float angle = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angle * Mathf.Rad2Deg;

            GameObject bullet =
                Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

            rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
        }
    }
    
    private void TargetedShoot()
    {
        if (CooldownChecker())
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angle * Mathf.Rad2Deg;

            GameObject bullet = Instantiate(bigEnemyBulletPrefab, transform.position,Quaternion.Euler(0, 0, angleDegrees));

            rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bigBulletSpeed * Time.fixedDeltaTime;
        }
    }
}