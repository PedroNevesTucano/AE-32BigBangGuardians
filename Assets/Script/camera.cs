using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    private float followSpeed = 10f;
    private float mouseOffset = 3f;
    public GameObject sniper;
    public float distance;

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
        Vector3 targetPosition = player.transform.position + initialOffset;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = player.transform.position.z;

        Vector3 offsetToMouse = (mousePosition - player.transform.position).normalized * mouseOffset;

        distance = Vector2.Distance(player.transform.position, mousePosition);

        if (isRightMouseButtonDown == true && player.dead == false && sniper.activeSelf)
        {
            targetPosition += offsetToMouse;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime); // movimentação da câmara


            if (distance < 3)
            {
                //Debug.Log("Distancia!");
                mouseOffset = 0f; followSpeed = 4f;
            }
            else
            {
                mouseOffset = 3f; followSpeed = 10f;
            }
        }
        if (isRightMouseButtonDown == false || player.dead || !sniper.activeSelf)
        {
            targetPosition = player.transform.position + initialOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
        }
    }
}