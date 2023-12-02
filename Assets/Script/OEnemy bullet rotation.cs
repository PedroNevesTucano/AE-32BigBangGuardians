using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OEnemybulletrotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotz;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotz += 1000 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rotz);

        Destroy(gameObject, 5f);
    }
}
