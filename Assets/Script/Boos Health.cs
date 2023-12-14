using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosHealth : MonoBehaviour
{
    public Boss Boss;
    public Image healthBarImage;

    void Start()
    {

        healthBarImage = GetComponent<Image>();
    }

    void Update()
    {
        float fillAmount = CalculateFillAmount(Boss.health);
        healthBarImage.fillAmount = fillAmount;
    }

    float CalculateFillAmount(float currentHealth)
    {
        return currentHealth / 200;
    }
}
