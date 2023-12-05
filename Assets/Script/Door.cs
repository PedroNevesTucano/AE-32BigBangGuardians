using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int numberofenemies;
    public GameObject terminal;

    private void Awake()
    {
        numberofenemies = 0;
    }
    public void OnEnemySpawned()
    {
       print("Hello");
       numberofenemies += 1;
    }

    public void OnEnemyDestroyed()
    {
        print("Bye");
        numberofenemies -= 1;

        if (numberofenemies == 0)
        {
            // All enemies are destroyed, perform door destruction or any other action
            Destroy(gameObject);
            Instantiate(terminal, transform.position, transform.rotation);
        }
    }
}

 