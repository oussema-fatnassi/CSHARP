using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerInRange;
    public GameObject continueButton;

    void Update()
    {
        // Show the dialogue only when the player presses E and is in range
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        // Enable continue button once the current dialogue line is finished
        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Don't activate the dialoguePanel here, wait for E key press
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialoguePanel.SetActive(false);
            zeroText();
        }
    }
}
