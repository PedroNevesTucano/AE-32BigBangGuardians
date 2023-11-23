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
    
    private protected abstract void Shoot();
    
    private protected void Reload()
    {
        capacity = maxCapacity;
    }
}
