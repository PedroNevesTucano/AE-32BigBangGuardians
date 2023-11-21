using UnityEngine;
using UnityEngine.UI;

public class ShotgunUIUpdate : MonoBehaviour
{
    public Weapon_Switcher WeaponSwitcher;
    public Image ShotgunUIIndicator;
    public Shotgun shotgun;

    void Start()
    {
        WeaponSwitcher = FindObjectOfType<Weapon_Switcher>();
        ShotgunUIIndicator = GetComponent<Image>();
    }

    void Update()
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
