using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class TerminalScript : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI terminalTextMesh;
    public Button terminalbutton;
    public bool alreadyhacked = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < 3 && Input.GetKeyDown(KeyCode.E) && alreadyhacked == false)
        {
            terminalTextMesh.gameObject.SetActive(true);
            terminalbutton.gameObject.SetActive(true);
            alreadyhacked = true;
        }

    }

    public void DisableUI()
    {
        terminalTextMesh.gameObject.SetActive(false);
        terminalbutton.gameObject.SetActive(false);
    }
}
