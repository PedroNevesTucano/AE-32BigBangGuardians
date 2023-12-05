    using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueSystemNPC: MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button optionButtonLeft;
    public Button optionButtonRight;
    public DialogueNode currentNode;
    public bool dialogueended;
    public float timer;
    public GameObject player;
    public TextMeshProUGUI triggertext;


    void Start()
    {
        InitializeDialogueTree();
        optionButtonLeft.gameObject.SetActive(false);
        optionButtonRight.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    void InitializeDialogueTree()
    {
        DialogueNode node1 = new DialogueNode("Hey! Long time no see. How've you been?");
        DialogueNode node2 = new DialogueNode("Yes I am, got crazy and know he wants to stop the BigBang");
        DialogueNode node4 = new DialogueNode("He drank to much vodka and smoked to many cigarets");
        DialogueNode node3 = new DialogueNode("Then bye");

        node1.LeftOption = new DialogueOption("Aren't you the brother of the crazy Dr.", node2);
        node1.RightOption = new DialogueOption("No Time to talk", node3);

        node2.LeftOption = new DialogueOption("Why?", node4);


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
            timer = 0;
        }

        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < 3 && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
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
