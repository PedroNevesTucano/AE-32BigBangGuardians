using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player; 
    public float followSpeed = 10f; 
    public float mouseOffset = 3f;
    public GameObject sniper;

    private Vector3 initialOffset;
    private bool isRightMouseButtonDown = false;

    void Start()
    {
        initialOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && sniper.activeSelf && player.dead == false)
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
        if (isRightMouseButtonDown == true && player.dead == false && sniper.activeSelf)
        {
            Vector3 targetPosition = player.transform.position + initialOffset;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = player.transform.position.z;

            Vector3 offsetToMouse = (mousePosition - player.transform.position).normalized * mouseOffset;

            targetPosition += offsetToMouse;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
        }
        if (isRightMouseButtonDown == false || player.dead || !sniper.activeSelf)
        {
            Vector3 targetPosition = player.transform.position + initialOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
        }
    }
}