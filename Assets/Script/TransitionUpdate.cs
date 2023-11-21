using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionUpdate : MonoBehaviour
{
    public Image TransitionImage;
    public HubTriggerScript hubTrigger;
    public Lv1 lv1Trigger;
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
            currentAmount = Mathf.Clamp(currentAmount - Time.deltaTime * 200, 0f, 100f);
            TransitionImage.fillAmount = currentAmount / 100f;
        }
        if (currentAmount <= 0 && onStart == true)
        {
            onStart = false;
        }

        if (hubTrigger != null && hubTrigger.switchToTransition == true)
        {
            currentAmount = Mathf.Clamp(currentAmount + Time.deltaTime * 200, 0f, 100f);
            TransitionImage.fillAmount = currentAmount / 100f;
        }

        if (hubTrigger != null && hubTrigger.switchToTransition == true && currentAmount == 100)
        {
            SceneManager.LoadScene("HUB");
        }

        if (lv1Trigger != null && lv1Trigger.switchToTransitionLv1 == true)
        {
            currentAmount = Mathf.Clamp(currentAmount + Time.deltaTime * 200, 0f, 100f);
            TransitionImage.fillAmount = currentAmount / 100f;
        }

        if (lv1Trigger != null && lv1Trigger.switchToTransitionLv1 == true && currentAmount == 100)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
