using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap") || collision.CompareTag("BulletTag") || collision.CompareTag("Enemy") || collision.CompareTag("EnemyBulletTag") || collision.CompareTag("BigBulletTag") || collision.CompareTag("BigEnemyBulletTag") || collision.CompareTag("SpawnTrigger"))
        {
            return;
        }
        else if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player.IsDashing() || player.iFrames > 0)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
