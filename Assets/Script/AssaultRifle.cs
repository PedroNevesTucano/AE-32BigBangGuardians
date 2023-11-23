using UnityEngine;
public class AssaultRifle : AbstractWeapon
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
    }
    
    private protected override void Shoot()
    {
        if (Input.GetMouseButton(0)&& counter <= 0 && capacity > 0)
        {
            capacity -= 1;
            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + 90));

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 initialDirection = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.up;
            rb.velocity = initialDirection * bulletSpeed * Time.fixedDeltaTime;
            isshooting = true;
        }
    }
}