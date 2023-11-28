using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : AbstractWeapon
{
    private void FixedUpdate()
    {
        Shoot();
    }

    private void Update()
    {

        if (isshooting)
        {
            counter += Time.deltaTime;
        }

        if (counter >= cooldown)
        {
            counter = 0;
            isshooting = false;
        }
        
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Reload();
        }
    }
    protected override void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && counter <= 0 && capacity > 0)
        {
           
            // Instantiate bullets with the desired rotation
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + 90));
            GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + 10 + 90));
            GameObject bullet3 = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - 10 + 90));

            // Get Rigidbody components
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D rb1 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = bullet3.GetComponent<Rigidbody2D>();

            // Set the velocity of bullets
            Vector2 initialDirection = (Vector2)(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.up);
            Vector2 initialDirection2 = (Vector2)(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 10) * Vector3.up);
            Vector2 initialDirection3 = (Vector2)(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 10) * Vector3.up);

            rb.velocity = initialDirection * bulletSpeed * Time.fixedDeltaTime;
            rb1.velocity = initialDirection2 * bulletSpeed * Time.fixedDeltaTime;
            rb2.velocity = initialDirection3 * bulletSpeed * Time.fixedDeltaTime;
            isshooting = true;
            capacity -= 1;
        }
    }
    
    protected void Reload()
    {
        base.Reload();
    }
}