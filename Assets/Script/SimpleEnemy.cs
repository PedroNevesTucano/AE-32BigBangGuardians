using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Pathfinding;

public class SimpleEnemy : AbstractEnemy
{
    //two fields with a private modifier to ensure that they can only be used within this class
    private float movementSpeed = 300;
    private bool chasing;
    public float nextwaipointdistance = 3f;
    private Path path;
    private int currentWaipoint = 0;
    bool reachedendofpath = false;
    Seeker seeker;
    
    /*
        Awake,Start,Update,FixedUpdate,OnTriggerEnter2D methods are inherited from MonoBehaviour 
        which is inherited through the parent AbstractEnemy class 
        and have a private access modifier to ensure that the implementation of this method of this class 
        will not be inherited by another class 
    */
    private void Awake()
    {
        bulletCooldownBase = bulletCooldown;
        bulletCooldown = 2f;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        seeker = GetComponent<Seeker>();
        door = GameObject.FindGameObjectWithTag("door");
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void OnpathComplete(Path P)
    {
        if (!P.error)
        {
            path = P;
            currentWaipoint = 0;
        }
    }

    private void Update()
    {
        if (path == null)
        {
            // do nothing
        }
        if(currentWaipoint >= path.vectorPath.Count)
        {
            reachedendofpath = true;
        }
        else
        {
            reachedendofpath = false;
        }

        if (health <= 0)
        {
            if (door != null) 
            { 
                door.GetComponent<Door>().OnEnemyDestroyed(); 
            }
            Destroy(gameObject);
        }
        bulletCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        Vector3 movementDirection = directionToPlayer.normalized;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaipoint] - rb.position).normalized;
        Vector2 force = direction * movementSpeed * Time.fixedDeltaTime;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);

        if (distance < nextwaipointdistance)
        {
            currentWaipoint++;
        }

        rb.velocity = force;

        if (bulletCooldown < 0.2f)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0f, 0f, 1f);
        }
        Shoot();
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.transform.position, OnpathComplete);
        }
    }

    //Overriding an abstract method from the parent AbstractEnemy class
    protected override void Shoot()
    {
        if (CooldownChecker())
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angle * Mathf.Rad2Deg;

            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = bulletSpeed * Time.fixedDeltaTime * direction;
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
    //protected override void OnTriggerEnter2D(Collider2D collision) => base.OnTriggerEnter2D(collision);
}