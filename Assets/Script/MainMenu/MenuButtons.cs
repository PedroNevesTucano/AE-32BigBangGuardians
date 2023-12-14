using Unity.VisualScripting;
//Just for testing,my friends 
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
//////////////////
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public class MenuButtons : MonoBehaviour
{
    private FadeInAndOut fadeInOut;
    public Button playButton;
    public Button quitButton;
    private bool isPlayButtonClicked;

    void Start()
    {
        fadeInOut = gameObject.AddComponent<FadeInAndOut>();
        
        if(playButton == null) {
            GameObject playButtonObj = GameObject.FindGameObjectWithTag("PlayButton");
            if(playButtonObj != null) {
                playButton = playButtonObj.GetComponent<Button>();
            }
        }

        if(playButton != null) {
            playButton.onClick.AddListener(playButtonIsClicked);
        }
        
        
        if(quitButton == null) {
            GameObject quitButtonObj = GameObject.FindGameObjectWithTag("QuitButton");
            if(quitButtonObj != null) {
                quitButton = quitButtonObj.GetComponent<Button>();
            }
        }

        if(quitButton != null) {
            quitButton.onClick.AddListener(Quit);
        }
    }

    void FixedUpdate()
    {
        if (isPlayButtonClicked)
        {
            fadeInOut.UpdateAlpha(Time.fixedDeltaTime,-1);
        }
        else
        {
            fadeInOut.UpdateAlpha(Time.fixedDeltaTime,1);
        }
        
        if (fadeInOut.currentAlpha >= 1f && isPlayButtonClicked)
        {
            SceneManager.LoadScene("HUB");
            isPlayButtonClicked = false;
        }
    }

    private void playButtonIsClicked()
    {
        isPlayButtonClicked = true;
    }

    private void Quit()
    {
        //Still for testing in the editor, guys 
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        // Exiting an exe'd game
        Application.Quit();
        #endif
    }
}
