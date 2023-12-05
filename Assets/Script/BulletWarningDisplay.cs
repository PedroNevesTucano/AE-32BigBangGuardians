using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletWarningDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public Sniper sniper;
    public Shotgun shotgun;
    public AssaultRifle rifle;
    private TextMeshProUGUI textMeshPro;
    public Weapon_Switcher WeaponSwitcher;
    public float blinking = 1f;
    public float blinkingBase;
    public bool blinkingCondition;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.enabled = false;
        blinkingBase = blinking;
        blinking = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (sniper.capacity == 0 && WeaponSwitcher.currentWeapon == 0) 
        {
            blinkingCondition = true;
        }
        else if (shotgun.capacity == 0 && WeaponSwitcher.currentWeapon == 1)
        {
            blinkingCondition = true;
        }
        else if (rifle.capacity == 0 && WeaponSwitcher.currentWeapon == 2)
        {
            blinkingCondition = true;
        }
        else
        {
            textMeshPro.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (blinkingCondition)
        {
            blinking -= Time.fixedDeltaTime;
        }
        if (blinking <= 0f)
        {
            blinking = blinkingBase;
        }
        
        if (blinking <= 0.5f)
        {
            textMeshPro.enabled = true;
        }
        else 
        {
            textMeshPro.enabled = false;
        }
    }
}
