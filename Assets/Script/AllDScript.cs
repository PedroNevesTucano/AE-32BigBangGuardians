using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public GameObject EnemyBulletPrefab;
    public float bulletSpeed = 40f;
    public float bulletCooldown;
    public float bulletCooldownBase;
    public bool switchDirection;
    public float trapHealth;
    public float viewRange;

    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 0;
    }
    void Start()
    {
        switchDirection = false;
    }
    private void FixedUpdate()
    {
        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.fixedDeltaTime;
        }
        void Shoot()
        {
            if (0 < bulletCooldown)
            {
                return;
            }
            bulletCooldown = bulletCooldownBase;

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

        void ShootDirection(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angle * Mathf.Rad2Deg;

            GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * bulletSpeed * Time.fixedDeltaTime;
        }

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (trapHealth > 0)
        {
            if (distanceToPlayer <= viewRange)
            {
                Shoot();
            }
            if (trapHealth <= 39)
            {
                Shoot();
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletTag"))
        {
            trapHealth -= 15;
        }
        if (collision.CompareTag("BigBulletTag"))
        {
            trapHealth -= 30;
        }
    }
}
