using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Playerrotation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Transform parentTransform = transform.parent;
        Vector2 parentPosition = parentTransform.position;

        Vector2 mouseposition = Input.mousePosition;
        mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);

        float distance = Vector2.Distance(parentPosition, mouseposition);


        if (distance > 0.5)
        {
            Vector2 direction = new Vector2(mouseposition.x - transform.position.x, mouseposition.y - transform.position.y);

            transform.up = direction;
        }
    }
}
