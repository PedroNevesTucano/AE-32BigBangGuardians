using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

//public class Bullet : MonoBehaviour
//{
//    // Start is called before the first frame update
//    public float speed = 100f;
//    private Quaternion initialRotation;
//    void Start()
//    {
//        initialRotation = transform.rotation;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Calculate the bullet's movement direction based on the player's rotation
//        Vector3 movementDirection = initialRotation * Vector3.up;

//        // Calculate the new position based on speed and time
//        Vector3 newPosition = transform.position + movementDirection * speed * Time.deltaTime;

//        // Set the new position
//        transform.position = newPosition;
//    }
//}
public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    private Rigidbody2D rb;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRotation = transform.rotation;
        
        // Calculate the initial direction as a Vector2 from the initial rotation
        Vector2 initialDirection = (Vector2)(initialRotation * Vector3.up);
        
        // Apply an initial force in the direction of the initial rotation
        rb.AddForce(initialDirection * speed, ForceMode2D.Impulse);

        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) 
        {
            Destroy(gameObject);
        }
    }
}