using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanDashUpdate : MonoBehaviour
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

        myTextMesh.text = "Can Dash: " + player.canDash.ToString();
    }
}
