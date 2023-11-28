using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    protected bool isshooting = false;
    public float cooldown;
    public float counter;
    public int capacity;
    public int maxCapacity;
    
    protected abstract void Shoot();
    
    protected virtual void Reload()
    {
        capacity = maxCapacity;
    }
}
