using UnityEngine;
using UnityEngine.UI;

public class HPUpdate : MonoBehaviour
{
    public Player player;
    public Image healthBarImage;

    void Start()
    {
        player = FindObjectOfType<Player>();
        healthBarImage = GetComponent<Image>();
    }

    void Update()
    {
        if (player.playerHealth <= 0)
        {
            player.playerHealth = 0;
        }

        float fillAmount = CalculateFillAmount(player.playerHealth);
        healthBarImage.fillAmount = fillAmount;

        if (player.playerHealth >= 67)
        {
            healthBarImage.color = Color.green;
        } else if (player.playerHealth >= 34 && player.playerHealth < 67)
        {
            healthBarImage.color = Color.yellow;
        }else if (player.playerHealth <34)
        {
            healthBarImage.color = Color.red;
        }
    }

    float CalculateFillAmount(float currentHealth)
    {
        return currentHealth / 100;
    }
}
