using UnityEngine;
public class ExplosiveEnemyScript : AbstractEnemy
{
    //two fields with a private modifier to ensure that they can only be used within this class
    public GameObject explosionArea;
    public GameObject explosionIndicator;

    private float movementSpeed = 400;
    private bool chasing;
    private bool explode;
    private float timeBeforeExplosion = 1.3f;

    private float IncreaseSize;
    /*
        Awake,Start,Update,FixedUpdate,OnTriggerEnter2D methods are inherited from MonoBehaviour 
        which is inherited through the parent AbstractEnemy class 
        and have a private access modifier to ensure that the implementation of this method of this class 
        will not be inherited by another class 
    */
    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 1.5f;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
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

        if (explosionArea.transform.localScale.x >= 6)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f, 1f);
            timeBeforeExplosion -= Time.fixedDeltaTime;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.0824f, 0.7843f, 0.2549f, 1f);
        }

        if (timeBeforeExplosion < 0.3f)
        {
            explosionArea.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.1f, 0,1f);
        }

        if (timeBeforeExplosion <= 0)
        {
            Destroy(gameObject);
        }

        if (chasing || health <= 49)
        {
            if (distanceToPlayer >= 3)
            {
                rb.velocity = movementSpeed * Time.fixedDeltaTime * movementDirection;
            }
            else
            {
                explode = true;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (explode == true) 
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            Shoot();
            rb.velocity = Vector3.zero;
        }
    }

    //Overriding an abstract method from the parent AbstractEnemy class
    protected override void Shoot()
    {
        IncreaseSize += Time.fixedDeltaTime;

        if (explosionArea.transform.localScale.x <= 6)
        {
            explosionArea.transform.localScale = new Vector3(IncreaseSize * 9f, IncreaseSize * 9f, 1f);
        }
        
        if (explosionIndicator.transform.localScale.x <= 6)
        {
            explosionIndicator.transform.localScale = new Vector3(IncreaseSize * 14f, IncreaseSize * 14f, 1f);
        }
    }
    /*
    Keywords new and override are used to implement polymorphism , but they have significant differences.
    
    The new keyword is used for method hiding in a derived class.
    It's not an override; instead, it creates a new method with the same name as the one in the base class.
    The hidden method doesn't necessarily have to be marked as virtual (virtual) in the base class.
    A method in the derived class marked with new won't be called when using
    a base class variable for an object of the derived class. The base class method will be called.
    */
    private new void OnTriggerEnter2D(Collider2D collision) => base.OnTriggerEnter2D(collision);
}