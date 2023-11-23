using UnityEngine;
using UnityEngine.UI;

public class GunCooldownUIUpdate : MonoBehaviour
{
    public enum UIType
    {
        Sniper,
        Charged,
        Shotgun
    }

    public Weapon_Switcher WeaponSwitcher;
    public Image SniperUIIndicator;
    public Image ChargedUI;
    public Sniper sniper;
    public Image ShotgunUIIndicator;
    public Shotgun shotgun;

    public UIType uiType;  // Add a variable to specify the UI type

    void Start()
    {
        WeaponSwitcher = FindObjectOfType<Weapon_Switcher>();
        SniperUIIndicator = GetComponent<Image>();
        ChargedUI = GetComponent<Image>();
        ShotgunUIIndicator = GetComponent<Image>();
    }

    void Update()
    {
        if (uiType == UIType.Sniper)
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
        else if (uiType == UIType.Charged)
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
        }else if (uiType == UIType.Shotgun)
        {

            if (shotgun.counter <= 0)
            {
                ShotgunUIIndicator.color = Color.blue;
            }
            else
            {
                ShotgunUIIndicator.color = Color.black;
            }

            if (WeaponSwitcher.currentWeapon == 1)
            {
                ShotgunUIIndicator.enabled = true;
            }
            else
            {
                ShotgunUIIndicator.enabled = false;
            }
        }
    }
}
