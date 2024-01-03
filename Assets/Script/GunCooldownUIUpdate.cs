using UnityEngine;
using UnityEngine.UI;

public class GunCooldownUIUpdate : MonoBehaviour
{
    public enum UIType
    {
        Sniper,
        Charged,
        Shotgun,
        Rifle
    }

    public Weapon_Switcher WeaponSwitcher;
    public Image SniperUIIndicator;
    public Image ChargedUI;
    public Sniper sniper;
    public Image ShotgunUIIndicator;
    public Shotgun shotgun;
    public Image RilfeUIIndicator;
    public AssaultRifle rifle;

    public Sprite coldown1Active;
    public Sprite coldown1Inactive;
    public Sprite coldown2Active;
    public Sprite coldown2Inactive;

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
                SniperUIIndicator.sprite = coldown1Active;
            }
            else
            {
                SniperUIIndicator.sprite = coldown1Inactive;
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
                ChargedUI.sprite = coldown2Active;
            }
            else
            {
                ChargedUI.sprite = coldown2Inactive;
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
                ShotgunUIIndicator.sprite = coldown1Active;
            }
            else
            {
                ShotgunUIIndicator.sprite = coldown1Inactive;
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
        else if (uiType == UIType.Rifle)
        {

            if (rifle.counter <= 0)
            {
                RilfeUIIndicator.sprite = coldown1Active;
            }
            else
            {
                RilfeUIIndicator.sprite = coldown1Inactive;
            }

            if (WeaponSwitcher.currentWeapon == 2)
            {
                RilfeUIIndicator.enabled = true;
            }
            else
            {
                RilfeUIIndicator.enabled = false;
            }
        }
    }
}
