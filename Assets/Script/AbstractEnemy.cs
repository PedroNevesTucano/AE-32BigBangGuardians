using UnityEngine;
public abstract class AbstractEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject enemyBulletPrefab;
    public float bulletSpeed;
    public float bulletCooldown;
    public float bulletCooldownBase;
    public float viewRange;
    public float health;
    public Rigidbody2D rb;
    private protected abstract void Awake();
    private protected abstract void FixedUpdate();
    private protected abstract void Shoot();
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletTag"))
        {
            health -= 15;
        }
        if (collision.CompareTag("BigBulletTag"))
        {
            health -= 30;
        }
    }
}