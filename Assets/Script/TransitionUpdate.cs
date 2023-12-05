using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionUpdate : MonoBehaviour
{
    public Image TransitionImage;
    public HubTriggerScript hubTrigger;
    public TutorialTransition tutorial;
    public Lv1 lv1Trigger;
    public Player player;
    public float currentAmount = 100f;
    public bool onStart;

    void Start()
    {
        TransitionImage = GetComponent<Image>();
        onStart = true;
    }

    void Update()
    {
        if (onStart)
        {
            OpenTransition();
        }
        if (currentAmount <= 0 && onStart == true)
        {
            onStart = false;
        }

        if (hubTrigger != null && hubTrigger.switchToTransition == true)
        {
            CloseTransition();
        }

        if (hubTrigger != null && hubTrigger.switchToTransition == true && currentAmount == 100)
        {
            SceneManager.LoadScene("HUB");
        }

        if (lv1Trigger != null && lv1Trigger.switchToTransitionLv1 == true)
        {
            CloseTransition();
        }

        if (tutorial != null && tutorial.switchToTutorialTransition == true  && currentAmount == 100)
        {
            SceneManager.LoadScene("Tutorial");
        }
        
        if (tutorial != null && tutorial.switchToTutorialTransition == true)
        {
            CloseTransition();
        }

        if (lv1Trigger != null && lv1Trigger.switchToTransitionLv1 == true && currentAmount == 100)
        {
            SceneManager.LoadScene("Level1");
        }

        if (player.dead == true)
        {
            CloseTransition();
        }

        if (player.dead && currentAmount == 100)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    private void OpenTransition() 
    {
        currentAmount = Mathf.Clamp(currentAmount - Time.deltaTime * 200, 0f, 100f);
        TransitionImage.fillAmount = currentAmount / 100f;
    }

    private void CloseTransition()
    {
        currentAmount = Mathf.Clamp(currentAmount + Time.deltaTime * 200, 0f, 100f);
        TransitionImage.fillAmount = currentAmount / 100f;
    }
}
