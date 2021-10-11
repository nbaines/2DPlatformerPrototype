using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeBox : MonoBehaviour
{
    public GameObject uiDialogueBox;
    public TMP_Text uiDialogueText;
    public string dialogueActual = "";

    public GameObject uiInteractKey;
    public bool playerIsInRange;

    // Update is called once per frame
    void Update()
    {
        if (playerIsInRange && Input.GetKeyDown(KeyCode.E)) //Player is within range of sign & presses 'E' key
        {
            if (uiDialogueBox.activeInHierarchy) //If dialogue box already open, then close it
            {
                uiDialogueBox.SetActive(false);
            }
            else //If dialogue box not open, then open it & set assigned text
            {
                uiDialogueBox.SetActive(true);
                uiDialogueText.text = dialogueActual;
            }
        }
    }

    //Invocation: Called when another collider w/ a RigidBody2D enters base object's collider
    //Use: Determine if Player is entering sign's reading range (i.e. collider)
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }

    //Invocation: Called when another collider w/ a RigidBody2D exits base object's collider
    //Use: Close dialogue box and disable Interaction key when player exits sign's reading range (i.e. collider)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerIsInRange = false;
            uiDialogueBox.SetActive(false);
            uiInteractKey.SetActive(false);
        }
    }

    //Invocation: Called when another collider w/ a RigidBody2D stays within base object's collider
    //Use: Show Interaction key (default 'E') if Player stays within sign's reading range (i.e. collider)
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            uiInteractKey.SetActive(true);
        }
    }
}
