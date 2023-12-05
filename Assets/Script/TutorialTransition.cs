using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTransition : MonoBehaviour
{
    private bool Switch;
    public bool switchToTutorialTransition;
    public TextMeshProUGUI myTextMesh;
    private string fullText = "(E) Enter Tutorial";
    private float textSpeed = 0.02f;

    
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Switch == true)
        {
            switchToTutorialTransition = true;
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