using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI myTextMesh;
    public Player player;
    void Start()
    {
        myTextMesh = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerHealth <= 0)
        {
            player.playerHealth = 0;
        }

        myTextMesh.text = "Health: " + player.playerHealth.ToString();
    }
}
