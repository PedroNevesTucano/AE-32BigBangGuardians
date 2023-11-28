using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Hashtable textHashtable = new Hashtable();
    private int switchingCounter;

    public TextMeshProUGUI myTextMesh;
    private float textSpeed = 0.02f;
    private float timerDuration = 1.5f;
    private float timer;
    
    public Weapon_Switcher WeaponSwitcher;
    
    private void Start()
    {
        InitializeTextHashtable();
        WeaponSwitcher = FindObjectOfType<Weapon_Switcher>();

        StartCoroutine(NextText());
    }

    void Update()
    {
        HandleMovementInput(KeyCode.W, 1);
        HandleMovementInput(KeyCode.S, 2);
        HandleMovementInput(KeyCode.A, 3);
        HandleMovementInput(KeyCode.D, 4);
        UpdateTimer();
        HandleWeaponSwitching();
    }

    private void HandleMovementInput(KeyCode key, int counter)
    {
        bool movementKeyPressedAndHold = Input.GetKey(key);
        bool spaceKeyPressed = Input.GetKeyDown(KeyCode.Space);

        if ((movementKeyPressedAndHold && switchingCounter == counter) || (movementKeyPressedAndHold && spaceKeyPressed && switchingCounter == 5))
        {
            switchingCounter++;
            StartCoroutine(NextText());
        }
    }

    private void HandleWeaponSwitching()
    { 
        if (WeaponSwitcher.currentWeapon == 0)
        {
            if (Input.GetMouseButton(1) && switchingCounter == 6)
            {
                switchingCounter++;
                StartCoroutine(NextText());
            }
            if (Input.GetMouseButton(1) && Input.GetMouseButton(0) && switchingCounter == 7)
            {
                StartTimer();
                switchingCounter++;
                StartCoroutine(NextText());
            }
        }
    }

    private void StartTimer()
    {
        timer = timerDuration;
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
            switchingCounter++;
            StartCoroutine(NextText());
            timer = timerDuration; 
        }
        
        if (!Input.GetMouseButton(0))
        {
            timer = timerDuration;
        }
        Debug.Log(timer);
    }

    private void InitializeTextHashtable()
    {
        textHashtable.Add(0, "");
        textHashtable.Add(1, "Press \"W\" to move forward");
        textHashtable.Add(2, "Press \"S\" to move backward");
        textHashtable.Add(3, "Press \"A\" to move left");
        textHashtable.Add(4, "Press \"D\" to move right");
        textHashtable.Add(5, "Sometimes you need to accelerate to save your life. Press one of the movement and \"Space\" buttons at the same time to make a dash");
        textHashtable.Add(6, "You are holding a sniper rifle, press the right mouse button to aim");
        textHashtable.Add(7, "Now you're in the aiming mode, left click to shoot");
        textHashtable.Add(8, "Great, now without leaving the scope and hold down the left mouse button until the player turns bright blue and release the left button");
        textHashtable.Add(9, "Fire!");
    }

    private IEnumerator NextText()
    {
        if (textHashtable.ContainsKey(switchingCounter))
        {
            string nextText = (string)textHashtable[switchingCounter];
            yield return StartCoroutine(LetterByLetter(nextText));
        }
    }

    private IEnumerator LetterByLetter(string printedText)
    {
        for (int i = 0; i <= printedText.Length; i++)
        {
            myTextMesh.text = printedText.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}