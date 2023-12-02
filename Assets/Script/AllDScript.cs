using UnityEngine;
public class AllDScript : AbstractEnemy
{
    private bool switchDirection;
    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 0;
    }
    private void Start()
    {
        switchDirection = false;
    }
    private void FixedUpdate()
    {
        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.fixedDeltaTime;
        }
        
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (health > 0)
        {
            if (distanceToPlayer <= viewRange)
            {
                Shoot();
            }
            if (health <= 39)
            {
                Shoot();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void Shoot()
    {
        if (CooldownChecker())
        {
            if (switchDirection == false)
            {
                Vector3 rightDirection = Vector3.right;
                ShootDirection(rightDirection);

                Vector3 leftDirection = Vector3.left;
                ShootDirection(leftDirection);

                Vector3 downDirection = Vector3.down;
                ShootDirection(downDirection);

                Vector3 upDirection = Vector3.up;
                ShootDirection(upDirection);

                switchDirection = true;
            }
            else
            {
                Vector3 rightUpDirection = new Vector3(1f, 1f, 0f).normalized;
                ShootDirection(rightUpDirection);

                Vector3 rightDownDirection = new Vector3(1f, -1f, 0f).normalized;
                ShootDirection(rightDownDirection);

                Vector3 leftUpDirection = new Vector3(-1f, 1f, 0f).normalized;
                ShootDirection(leftUpDirection);

                Vector3 leftDownDirection = new Vector3(-1f, -1f, 0f).normalized;
                ShootDirection(leftDownDirection);

                switchDirection = false;
            }
        }
    }
    
    
    private void ShootDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angle * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));
        
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * bulletSpeed * Time.fixedDeltaTime;
    }
}
