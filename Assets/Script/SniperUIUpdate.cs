using UnityEngine;
using UnityEngine.UI;

public class SniperUIUpdate : MonoBehaviour
{
    public Weapon_Switcher WeaponSwitcher;
    public Image SniperUIIndicator;
    public Sniper sniper;

    void Start()
    {
        WeaponSwitcher = FindObjectOfType<Weapon_Switcher>();
        SniperUIIndicator = GetComponent<Image>();
    }

    void Update()
    {
        if (sniper.bulletCooldown <= 0)
        {
            SniperUIIndicator.color = Color.blue;
        }
        else
        {
            SniperUIIndicator.color = Color.black;
        }

        if (WeaponSwitcher.currentWeapon == 0)
        {
            SniperUIIndicator.enabled = true;
        }
        else
        {
            SniperUIIndicator.enabled = false;
        }
    }
}
