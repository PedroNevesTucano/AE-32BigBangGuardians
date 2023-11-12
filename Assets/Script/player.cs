using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float xAxyx, yAxyx;
    public float dashSpeed;
    private float dashDuration = 0.3f;
    public float dashDurationTimer;
    public float dashCooldown;
    public float dashCooldownBase;
    public float playerHealth;
    private bool invincibility;
    public float iFrames = 0.3f;
    private float iFramesBase;
    private float iFramesBlinking = 0.1f;
    private float iFramesBlinkingBase;
    public bool canDash;
    public bool isPaused;

    private SpriteRenderer spriteRenderer;

    private ShootProjectile sniperScript;

    private Vector3 direction;

    private Rigidbody2D rb;

    private Vector3 dashDirection;

    [SerializeField] TrailRenderer tr;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashCooldownBase = dashCooldown;
        dashCooldown = 0;
        iFramesBase = iFrames;
        iFrames = 0;
        iFramesBlinkingBase = iFramesBlinking;
        iFramesBlinking = 0;
    }

    void Start()
    {
        sniperScript = GetComponent<ShootProjectile>();
        isPaused = false;
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
        invincibility = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            SceneManager.LoadScene("Level1");
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("Level1");
        }

        xAxyx = Input.GetAxisRaw("Horizontal");
        yAxyx = Input.GetAxisRaw("Vertical");
        direction = new Vector3(xAxyx, yAxyx, 0).normalized;

        Vector3 movement = Vector3.zero;

        if (movement != Vector3.zero && !IsDashing())
        {
            movement.Normalize();
        }

        if (0 < dashCooldown)
        {
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !IsDashing() && (xAxyx != 0 || yAxyx != 0))
        {
            if (0 < dashCooldown)
            {
                return;
            }
            dashCooldown = dashCooldownBase;
            StartDashing(direction);
            tr.emitting = true;
        }

        if (!IsDashing())
        {
            rb.velocity = movement * speed;
            tr.emitting = false;
        }

        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
        {
            invincibility = true;
        }
        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.P))
        {
            invincibility = false;
        }

        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
            {
                isPaused = true;
                Time.timeScale = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
            {
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.fixedDeltaTime;
        }
        if (!IsDashing() && sniperScript.isRightMouseButtonDown == false)
        {
            Vector3 movement = new Vector3(xAxyx, yAxyx, 0).normalized;
            rb.velocity = movement * speed * Time.fixedDeltaTime;
        } else if (!IsDashing() && sniperScript.isRightMouseButtonDown == true)
            {
            Vector3 movement = new Vector3(xAxyx, yAxyx, 0).normalized;
            rb.velocity = movement * speed / 1.3f * Time.fixedDeltaTime;
            }
        else
        {
            rb.velocity = dashDirection * dashSpeed * Time.fixedDeltaTime;
        }
        
        if (iFrames > 0)
        {
            iFrames -= Time.fixedDeltaTime;
            iFramesBlinking -= Time.fixedDeltaTime;
        }
        else
        {
            spriteRenderer.enabled = true;
        }

        if ((iFrames > 0 && iFramesBlinking <= 0))
        {
            spriteRenderer.enabled = true;
            iFramesBlinking = iFramesBlinkingBase;
        }
        else if (iFrames > 0 && iFramesBlinking > 0)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBulletTag") && !IsDashing() && invincibility == false && iFrames <= 0)
        {
            playerHealth -= 15;
            iFrames = iFramesBase;
        }
        if (collision.CompareTag("BigEnemyBulletTag") && !IsDashing() && invincibility == false && iFrames <= 0)
        {
            playerHealth -= 30;
            iFrames = iFramesBase;
        }
    }

    public bool IsDashing()
    {
        return dashDurationTimer > 0f;
    }

    private void StartDashing(Vector3 direction)
    {
        dashDirection = direction;
        dashDurationTimer = dashDuration;
        Invoke("StopDashing", dashDuration);
    }

    private void StopDashing()
    {
        dashDurationTimer = 0f;
    }
}
