using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCscript : MonoBehaviour
{
    public TextMeshProUGUI myTextMesh;
    public string fullText = "";
    private float textSpeed = 0.02f;
    public bool test;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        test = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LetterByLetter());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            myTextMesh.text = "";
            test = false;
        }
    }

    private IEnumerator LetterByLetter()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            if (test == false)
            {
                break;
            }
            myTextMesh.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
