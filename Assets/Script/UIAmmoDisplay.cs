using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAmmoDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI myTextMesh;
    public Weapon_Switcher WeaponSwitcher;
    public Sniper sniper;
    public Shotgun shotgun;
    public AssaultRifle rifle;

    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (WeaponSwitcher.currentWeapon == 0) 
        {
            myTextMesh.text = sniper.capacity.ToString() + " / " + sniper.maxCapacity.ToString();
        }
        else if (WeaponSwitcher.currentWeapon == 1)
        {
            myTextMesh.text = shotgun.capacity.ToString() + " / " + shotgun.maxCapacity.ToString();
        }
        else if (WeaponSwitcher.currentWeapon == 2)
        {
            myTextMesh.text = rifle.capacity.ToString() + " / " + rifle.maxCapacity.ToString();
        }
    }
}