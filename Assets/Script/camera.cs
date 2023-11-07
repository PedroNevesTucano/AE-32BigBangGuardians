using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 10f; 
    public float mouseOffset = 3f; 

    private Vector3 initialOffset;
    private bool isRightMouseButtonDown = false;

    void Start()
    {
        initialOffset = transform.position - player.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseButtonDown = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightMouseButtonDown = false;
        }
    }

    private void FixedUpdate()
    {
        if (isRightMouseButtonDown == true)
        {
            Vector3 targetPosition = player.position + initialOffset;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = player.position.z;

            Vector3 offsetToMouse = (mousePosition - player.position).normalized * mouseOffset;

            targetPosition += offsetToMouse;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
        }
        if (isRightMouseButtonDown == false)
        {
            Vector3 targetPosition = player.position + initialOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
        }
    }
}
