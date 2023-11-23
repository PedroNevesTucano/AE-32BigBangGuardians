using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Weapon_Switcher : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    public int currentWeapon = 0;

    void Update()
    {
        // Получаем ввод от колесика мыши
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // Если колесико мыши вращается вверх, увеличиваем индекс оружия
        if (scrollWheelInput > 0f)
        {
            SwitchWeapon(1);
        }
        // Если колесико мыши вращается вниз, уменьшаем индекс оружия
        else if (scrollWheelInput < 0f)
        {
            SwitchWeapon(-1);
        }
    }

    void SwitchWeapon(int direction)
    {
        // Скрываем все оружия
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }

        // Изменяем индекс текущего оружия
        currentWeapon += direction;

        // Обработка циклического переключения оружия
        if (currentWeapon < 0)
        {
            currentWeapon = transform.childCount - 1;
        }
        else if (currentWeapon >= transform.childCount)
        {
            currentWeapon = 0;
        }


        transform.GetChild(currentWeapon).gameObject.SetActive(true);
    }
}
