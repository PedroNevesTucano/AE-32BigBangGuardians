using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SupplyBoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Switch;
    public TextMeshProUGUI myTextMesh;
    public GameObject SupplyBox;
    public Player player;
    private string fullText = "(E) To Interact";
    private float textSpeed = 0.02f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Switch == true)
        {
            if (player.playerHealth < 100) {
                player.playerHealth += 45;
            }
            Destroy(SupplyBox);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Switch = true;
            StartCoroutine(LetterByLetter());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Switch = false;
            myTextMesh.text = "";
        }
    }

    private IEnumerator LetterByLetter()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            if (!Switch)
            {
                break;
            }
            myTextMesh.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}