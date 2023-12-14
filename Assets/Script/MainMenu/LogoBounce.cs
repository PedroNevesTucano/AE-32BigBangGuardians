using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoBounce : MonoBehaviour
{
    public float maxTopPosition;
    public float maxBottomPosition;
    private float followSpeed = 5f;
    public bool moveUp = true;

    void FixedUpdate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 currentPosition = rectTransform.anchoredPosition;

        if (moveUp)
        {
            currentPosition.y = Mathf.MoveTowards(currentPosition.y, maxTopPosition, Time.fixedDeltaTime * followSpeed);
        }
        else
        {
            currentPosition.y = Mathf.MoveTowards(currentPosition.y, maxBottomPosition, Time.fixedDeltaTime * followSpeed);
        }

        rectTransform.anchoredPosition = currentPosition;

        if (Mathf.Approximately(currentPosition.y, maxTopPosition) && moveUp)
        {
            moveUp = false;
        }
        if (Mathf.Approximately(currentPosition.y, maxBottomPosition) && !moveUp)
        {
            moveUp = true;
        }
    }
}
