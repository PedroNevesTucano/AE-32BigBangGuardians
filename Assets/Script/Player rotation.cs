using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Playerrotation : MonoBehaviour
{
    public Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Transform parentTransform = transform.parent;
        Vector2 parentPosition = parentTransform.position;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float distance = Vector2.Distance(parentPosition, mousePos);

        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        if (distance > 0.5)
        { 
            transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
        }
    }
}
