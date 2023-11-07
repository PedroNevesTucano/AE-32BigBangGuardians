using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public GameObject EnemyBulletPrefab;
    public GameObject BigEnemyBulletPrefab;
    public float bulletSpeed;
    public float bigBulletSpeed;
    public float bulletCooldown;
    public float bulletCooldownBase;
    public float bigBulletCooldown;
    public float bigBulletCooldownBase;
    public float trapHealth;

    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 0;
        bigBulletCooldownBase = bigBulletCooldown;
        bigBulletCooldown = 0;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (trapHealth > 0)
        {
            if (trapHealth >= 20)
            {
                Shoot();
            }
            else if (trapHealth <= 19)
            {
                TargetedShoot();
            }
        }
        void Shoot()
        {
            if (0 < bulletCooldown)
            {
                return;
            }
            bulletCooldown = bulletCooldownBase;

            Vector3 direction = Vector3.right;

            float angle = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angle * Mathf.Rad2Deg;

            GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
        }

        void TargetedShoot()
        {
            if (0 < bigBulletCooldown)
            {
                return;
            }
            bigBulletCooldown = bigBulletCooldownBase;

            // Set the direction vector to point to the right
            Vector3 direction = (player.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angle * Mathf.Rad2Deg;

            GameObject bullet = Instantiate(BigEnemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bigBulletSpeed * Time.fixedDeltaTime;
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