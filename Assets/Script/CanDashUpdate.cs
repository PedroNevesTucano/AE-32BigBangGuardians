using UnityEngine;
using UnityEngine.UI;

public class CanDashUpdate : MonoBehaviour
{
    public Player player;
    public Image staminaBarImage;

    void Start()
    {
        player = FindObjectOfType<Player>();
        staminaBarImage = GetComponent<Image>();
    }

    void Update()
    {
        if (player.canDash == true)
        {
            staminaBarImage.color = new Color(1f, 0.5647f, 0f, 1f); ;
        }
        else
        {
            staminaBarImage.color = new Color(0.3725f, 0.3725f, 0.3725f, 1f);
        }
    }
}
