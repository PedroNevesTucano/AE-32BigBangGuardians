using UnityEngine;
using UnityEngine.UI;

public class SelectedWeaponUpdate : MonoBehaviour
{
    public Weapon_Switcher WeaponSwitcher;
    public Image SelectedWeapon;
    void Start()
    {
        WeaponSwitcher = FindObjectOfType<Weapon_Switcher>();
        SelectedWeapon = GetComponent<Image>();
    }

    void Update()
    {
        if (WeaponSwitcher.currentWeapon == 0)
        {
            SelectedWeapon.color = Color.yellow;
        } else if (WeaponSwitcher.currentWeapon == 1)
        {
            SelectedWeapon.color = Color.red;
        }
    }
}
