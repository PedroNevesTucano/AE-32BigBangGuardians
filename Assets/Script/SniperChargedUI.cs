using UnityEngine;
using UnityEngine.UI;

public class SniperChargedUI : MonoBehaviour
{
    public Weapon_Switcher WeaponSwitcher;
    public Image ChargedUI;
    public Sniper sniper;

    void Start()
    {
        WeaponSwitcher = FindObjectOfType<Weapon_Switcher>();
        ChargedUI = GetComponent<Image>();
    }

    void Update()
    {
        if (sniper.bigBulletCooldown <= 0)
        {
            ChargedUI.color = new Color(0f, 0.9333f, 1f, 1f);
        }
        else
        {
            ChargedUI.color = Color.black;
        }

        if (WeaponSwitcher.currentWeapon == 0)
        {
            ChargedUI.enabled = true;
        }
        else
        {
            ChargedUI.enabled = false;
        }
    }
}
