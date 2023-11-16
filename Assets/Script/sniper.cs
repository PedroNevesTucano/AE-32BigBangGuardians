using UnityEditor.Build;
using UnityEngine;

public class Sniper : AbstractWeapon
{
 
    public GameObject bigBulletPrefab;
    public float bigBulletSpeed = 20f;
    public float bulletCooldown;
    public float bulletCooldownBase;
    public float bigBulletCooldown;
    public float bigBulletCooldownBase;
    public bool isHolding = false;
    public float holdBefore;
    public float holdBeforeBase;
    public float requiredHoldTime;
    public float requiredHoldTimeBase;
    public bool isRightMouseButtonDown = false;

    public bool shootFixed;
    public bool bigShootFixed;


    public Player playerScript;
    private SpriteRenderer playersprite;

    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 0;
        bigBulletCooldownBase = bigBulletCooldown;
        bigBulletCooldown = 0;
        requiredHoldTimeBase = requiredHoldTime;
        holdBeforeBase = holdBefore;
    }
    private void Start()
    {
        playersprite = playerScript.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.fixedDeltaTime;
        }
        if (bigBulletCooldown > 0)
        {
            bigBulletCooldown -= Time.fixedDeltaTime;
        }
        if (isHolding && bigBulletCooldown <= 0 && holdBefore <= 0)
        {
            requiredHoldTime -= Time.fixedDeltaTime;
        }
        if (isHolding)
        {
            holdBefore -= Time.fixedDeltaTime;
        }
        else
        {
            holdBefore = holdBeforeBase;
        }

        if (shootFixed)
        {
            Shoot();
            shootFixed = false;
        }

        if (bigShootFixed)
        {
            BigShoot();
            bigShootFixed = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerScript.IsDashing() && isRightMouseButtonDown == true)
        {
            isHolding = true;
        }

        if (playerScript.IsDashing() || isRightMouseButtonDown == false)
        {
            isHolding = false;
            requiredHoldTime = requiredHoldTimeBase;
        }

        if (isHolding == true && bigBulletCooldown <= 0 && holdBefore <= 0)
        {
            if (requiredHoldTime > 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 0f, 0f, 1f);
                playersprite.color = new Color(0.5f, 0f, 0f, 1f);

            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                playersprite.color = Color.red;
            }
        }
        else if (isHolding == true && bigBulletCooldown > 0 && holdBefore <= 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
            playersprite.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        else if (isHolding == true && bulletCooldown > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
            playersprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            playersprite.color = Color.black;
        }



        if (Input.GetMouseButtonUp(0) && isHolding && !playerScript.IsDashing())
        {
            if (requiredHoldTime <= 0 && bigBulletCooldown <= 0)
            {
                bigShootFixed = true;
                bulletCooldown = bulletCooldownBase;
            }
            else if (requiredHoldTime > 0)
            {
                shootFixed = true;
            }
            isHolding = false;
            requiredHoldTime = requiredHoldTimeBase;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseButtonDown = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightMouseButtonDown = false;
        }
    }

    private protected override void Shoot()
    {
        if (0 < bulletCooldown)
        {
            return;
        }
        bulletCooldown = bulletCooldownBase;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + 90));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 initialDirection = (Vector2)(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.up);
        rb.velocity = initialDirection * bulletSpeed * Time.fixedDeltaTime;
    }

    void BigShoot()
    {
        if (0 < bigBulletCooldown)
        {
            return;
        }
        bigBulletCooldown = bigBulletCooldownBase;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);

        float angleDegrees = angle * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bigBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bigBulletSpeed * Time.fixedDeltaTime;
    }
}