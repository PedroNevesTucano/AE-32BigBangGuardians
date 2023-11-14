using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lv1 : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Switch;
    public TextMeshProUGUI myTextMesh;
    private string fullText = "(E) Enter Level 1";
    private float textSpeed = 0.02f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Switch == true)
        {
            SceneManager.LoadScene("Level1");
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