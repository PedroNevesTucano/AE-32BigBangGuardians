using System.Collections;
using System.Collections.Generic;
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
    private protected abstract void Shoot();
}
