using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem: MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button optionButtonLeft;
    public Button optionButtonRight;
    public DialogueNode currentNode;
    public bool dialogueended;
    public float timer;


    void Start()
    {
        InitializeDialogueTree();
        optionButtonLeft.gameObject.SetActive(false);
        optionButtonRight.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    void InitializeDialogueTree()
    {
        DialogueNode node1 = new DialogueNode("Hello! How are you?");
        DialogueNode node2 = new DialogueNode("I'm good, thanks. How about you?");
        DialogueNode node3 = new DialogueNode("I'm not doing well. What about you?");
        DialogueNode node4 = new DialogueNode("Great!", "Good");
        DialogueNode node5 = new DialogueNode("That's not good. Anything I can do?");
        DialogueNode node6 = new DialogueNode("I'm sorry to hear that. Let me know if you need anything.", "Bad");

        node1.LeftOption = new DialogueOption("Good", node2);
        node1.RightOption = new DialogueOption("Not good", node3);

        node2.LeftOption = new DialogueOption("Great", node4);
        node2.RightOption = new DialogueOption("Not so great", node3);

        node3.LeftOption = new DialogueOption("Just exploring", node4);
        node3.RightOption = new DialogueOption("Looking for something", node5);

        node5.LeftOption = new DialogueOption("Yes, please.", node6);
        node5.RightOption = new DialogueOption("No, I'll be fine.", node6);

        currentNode = node1;
    }

    public void StartDialogue()
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = currentNode.DialogueText;
        DisplayOptions();
    }

    void ContinueDialogue(DialogueOption selectedOption)
    {
        currentNode = selectedOption.NextNode;
        dialogueText.text = currentNode.DialogueText;

        optionButtonLeft.gameObject.SetActive(false);
        optionButtonRight.gameObject.SetActive(false);

        if (currentNode.LeftOption != null || currentNode.RightOption != null)
        {
            DisplayOptions();
        }
        else
        {
            dialogueended = true;
        }
    }

    private void Update()
    {
        if (dialogueended)
        {
            timer += Time.deltaTime;
        }
        if (timer > 3) 
        {
            dialogueended = false;
            dialogueText.gameObject.SetActive(false);
        }
    }

    void DisplayOptions()
    {
        if (currentNode.LeftOption != null)
        {
            optionButtonLeft.gameObject.SetActive(true);
            optionButtonLeft.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.LeftOption.OptionText;
            optionButtonLeft.onClick.RemoveAllListeners();
            optionButtonLeft.onClick.AddListener(() => ContinueDialogue(currentNode.LeftOption));
        }

        if (currentNode.RightOption != null)
        {
            optionButtonRight.gameObject.SetActive(true);
            optionButtonRight.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.RightOption.OptionText;
            optionButtonRight.onClick.RemoveAllListeners();
            optionButtonRight.onClick.AddListener(() => ContinueDialogue(currentNode.RightOption));
        }
    }
}

public class DialogueNode
{
    public string DialogueText;
    public string? Finaloutcome; 
    public DialogueOption LeftOption;
    public DialogueOption RightOption;

    public DialogueNode(string text, string? finaloutcome = null)
    {
        DialogueText = text;
        Finaloutcome = finaloutcome;
    }
}

public class DialogueOption
{
    public string OptionText;
    public DialogueNode NextNode;

    public DialogueOption(string text, DialogueNode nextNode)
    {
        OptionText = text;
        NextNode = nextNode;
    }
}
