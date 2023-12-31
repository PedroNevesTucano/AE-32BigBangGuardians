using UnityEngine;
public abstract class AbstractEnemy : MonoBehaviour
{
    //all class fields are public for their further definition in the Unity editor
    
    public GameObject player;
    public GameObject enemyBulletPrefab;
    public float bulletSpeed;
    public float bulletCooldown;
    public float bulletCooldownBase;
    public float viewRange;
    public float health;
    public Rigidbody2D rb;
    protected GameObject door;

    /*private protected provides access to a class member
    from the class itself and from classes
    that are derived from this class and are in the same assembly
    */
    protected abstract void Shoot();
    protected bool CooldownChecker()
    {
        if (bulletCooldown > 0)
        {
            return false;
        }
        bulletCooldown = bulletCooldownBase;
        return true;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletTag"))
        {
            health -= 15;
        }
        else if (collision.CompareTag("BigBulletTag"))
        {
            health -= 30;
        }
        else if (collision.CompareTag("riflebullet"))
        {
            health -= 20;
        }
        else if (collision.CompareTag("shotgunbullet"))
        {
            health -= 10;
        }
    }
}