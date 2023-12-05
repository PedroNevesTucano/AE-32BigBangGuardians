using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("shotgunbullet") || collision.CompareTag("Oholder") || collision.CompareTag("Player") || collision.CompareTag("EnemyBulletTag") || collision.CompareTag("BulletTag") || collision.CompareTag("BigBulletTag") || collision.CompareTag("BigEnemyBulletTag") || collision.CompareTag("SpawnTrigger") || collision.CompareTag("Lv1"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
