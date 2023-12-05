using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private Stack<string> textStack = new();
    private float textSpeed = 0.009f;
    public TextMeshProUGUI myTextMesh;
    public Weapon_Switcher WeaponSwitcher;
    public Sniper Sniper;
    public GameObject target;
    private SpriteRenderer targetSpriteRenderer;
    
    private bool rightMouseButtonIsClicked;
    private bool leftMouseButtonIsClicked;
    private bool isDashed;
    private int numOfMessage;
    private bool isTargetHit;
    
    private float timer;
    private bool isTimerRunning;
    private void Start()
    {
        InitializeTextStack();
        StartCoroutine(NextText());
        WeaponSwitcher = FindObjectOfType<Weapon_Switcher>();
        Sniper = FindObjectOfType<Sniper>();
        targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (Sniper.playerScript.IsDashing())
        {
            isDashed = true;
        }
        
        
        if (textStack.Count > 0)
        {
            HandleMovementInput(KeyCode.W, "Press \"W\" to move forward", "Press \"S\" to move backward");
            HandleMovementInput(KeyCode.S, "Press \"S\" to move backward", "Press \"A\" to move left");
            HandleMovementInput(KeyCode.A, "Press \"A\" to move left", "Press \"D\" to move right");
            HandleMovementInput(KeyCode.D,"Press \"D\" to move right","Sometimes you need to accelerate to save your life. " +
                                                                      "Press one of the movement and \"Space\" buttons at the same time to make a dash");
            
            if (!isDashed && numOfMessage == 4 && (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)))
            {
                HandleMovementInput(KeyCode.Space,"Sometimes you need to accelerate to save your life. Press one of the movement and \"Space\" " +
                                                  "buttons at the same time to make a dash","");
            }

            if (isTimerRunning)
            {
                timer += Time.deltaTime;
            }

            if (WeaponSwitcher.currentWeapon != 0 && numOfMessage == 5)
            {
                HandleWeaponSwitching("", "Switch to a sniper rifle and press the right mouse button to aim");
            }
                
            if (WeaponSwitcher.currentWeapon == 0)
            {
                if (!rightMouseButtonIsClicked && numOfMessage == 5)
                {
                    HandleWeaponSwitching("", "You are holding a sniper rifle, press the right mouse button to aim");
                }

                if (Input.GetMouseButtonDown(1) && !rightMouseButtonIsClicked && numOfMessage == 6 )
                {
                    HandleWeaponSwitching("", "Now you are in aiming mode, point at the red target and left click to shoot");
                    rightMouseButtonIsClicked = true;
                }
                else if (Input.GetMouseButton(0) && !leftMouseButtonIsClicked && numOfMessage == 7)
                {
                    HandleWeaponSwitching("","Great, now without leaving the scope " +
                        "and hold down the " +
                        "left mouse button until the player turns bright blue and release the left button");
                    leftMouseButtonIsClicked = true;
                } 
                else if (Sniper.isHolding && Sniper.bigBulletCooldown <=0 && Sniper.holdBefore <= 0 && Sniper.requiredHoldTime <=0 && numOfMessage == 8 )
                {
                    HandleWeaponSwitching("","Fire!");
                }
                else if (numOfMessage == 9 && Sniper.capacity < Sniper.maxCapacity && Input.GetMouseButtonUp(0))
                {
                    HandleWeaponSwitching("Fire!","Press \"R\" for reload");
                }
                if (numOfMessage == 10 && Sniper.capacity == Sniper.maxCapacity)
                {
                    HandleWeaponSwitching("Press \"R\" for reload","Scroll up to switch to shotgun");
                }
            }
            
            else if (WeaponSwitcher.currentWeapon == 1){
                
                if (numOfMessage == 11)
                {
                    HandleWeaponSwitching("","Now you don't need to hold down the right mouse button to aim,just press the left mouse button to shoot");
                }
                else if(numOfMessage == 12 && Input.GetMouseButton(0))
                {
                    if (!isTimerRunning)
                    {
                        timer = 0f;
                        isTimerRunning = true;
                    }
                    HandleWeaponSwitching("","Remember that a shotgun is especially effective at close range and takes a long time to recharge");
                }
                else if (numOfMessage == 13 &&  timer >= 2.5f)
                {
                    HandleWeaponSwitching("", "Scroll up to switch to rifle");
                    isTimerRunning = false;
                }
            }
            
            else
            {
                if (numOfMessage == 14)
                {
                    HandleWeaponSwitching("","You don't need to hold down the right mouse button to aim,just press the left mouse button to shoot");
                }
                else if(numOfMessage == 15 && Input.GetMouseButton(0))
                {
                    if (!isTimerRunning)
                    {
                        timer = 0f;
                        isTimerRunning = true;
                    }
                    HandleWeaponSwitching("","The assault rifle is particularly effective at medium distances");
                }
                else if (numOfMessage == 16 && timer >= 2.5f)
                {
                    HandleWeaponSwitching("","Congratulations, the tutorial has been successfully completed");
                    isTimerRunning = false;
                    if (!isTimerRunning)
                    {
                        timer = 0f;
                        isTimerRunning = true;
                    }
                }
                else if (numOfMessage == 17 && timer >= 2.5f)
                {
                    HandleWeaponSwitching("","");
                    isTimerRunning = false;
                    SceneManager.LoadScene("HUB");
                }
            }
            //Debug.Log( numOfMessage);


            if (Input.GetKeyDown(KeyCode.K))
            {
                SceneManager.LoadScene("HUB");
            }
            
            
            
            
            if(timer >= 0.2f && isTargetHit)
            {
                targetSpriteRenderer.color = new Color(0.5f, 0.047f, 0.047f, 1f);
                isTimerRunning = false;
                isTargetHit = false;
            }
        }
    }

    private void HandleMovementInput(KeyCode key, string currentMessage, string nextMessage)
    {
        if (Input.GetKeyDown(key) && textStack.Peek() == currentMessage)
        {
            textStack.Pop(); // Remove the current message
            textStack.Push(nextMessage); // Add the next message
            numOfMessage++;
            StartCoroutine(NextText());
        }
    }

    private void HandleWeaponSwitching(string currentMessage,string nextMessage)
    {
        textStack.Pop(); // Remove the current message
        textStack.Push(nextMessage); // Add the next message
        numOfMessage++;
        StartCoroutine(NextText());
    }

    private void InitializeTextStack()
    {
        textStack.Push("Press \"W\" to move forward");
    }

    private IEnumerator NextText()
    {
        if (textStack.Count > 0)
        {
            string nextText = textStack.Peek(); // Use Peek to get the top element without removing it
            yield return StartCoroutine(LetterByLetter(nextText));
        }
    }

    private IEnumerator LetterByLetter(string printedText)
    {
        if (myTextMesh == null)
        {
            Debug.LogError("myTextMesh is not assigned!");
            yield break;
        }

        for (int i = 0; i <= printedText.Length; i++)
        {
            myTextMesh.text = printedText.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("BulletTag") || collision.CompareTag("BigBulletTag") && timer <= 3f))
        {
            targetSpriteRenderer.color = new Color(0f, 1f, 0f, 1f);
            isTargetHit = true;
            
            if (!isTimerRunning)
            {
                timer = 0f;
                isTimerRunning = true;
            }
        }
    }
}



