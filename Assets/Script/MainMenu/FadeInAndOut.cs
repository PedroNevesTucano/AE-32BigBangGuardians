using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInAndOut : MonoBehaviour
{
    public float fadeInOutDuration = 2f;
    public float currentAlpha = 1f;
    public UnityEngine.UI.Image image;

    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        SetImageAlpha(1f);
    }

    public void UpdateAlpha(float deltaTime,int plusOrMinus1)
    {
        currentAlpha -= plusOrMinus1 * (deltaTime / fadeInOutDuration);
        currentAlpha = Mathf.Clamp01(currentAlpha);
        SetImageAlpha(currentAlpha);
    }

    public void SetImageAlpha(float alpha)
    {
        Color currentColor = image.color;
        currentColor.a = alpha;
        image.color = currentColor;
    }
}