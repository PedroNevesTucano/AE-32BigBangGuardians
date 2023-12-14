using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilities : MonoBehaviour
{
    
    public GameObject blackHolePrefab;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BlackHoleSpawner();
        }
    }

    private void BlackHoleSpawner()
    {
        /*
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);

        float angleDegrees = angle * Mathf.Rad2Deg;

        GameObject blackHole = Instantiate(blackHolePrefab, transform.position, Quaternion.Euler(0, 0, angleDegrees));

        Rigidbody2D rb = blackHole.GetComponent<Rigidbody2D>();
        */
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; 

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        Instantiate(blackHolePrefab, worldPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
        else
        {
            return;
        }
    }
}
