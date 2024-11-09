using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text npcNameText;
    [SerializeField] Image npcImage;
    [SerializeField] string[] dialogue;
    [SerializeField] Sprite npcSprite;
    private int index = 0; 
    public float wordSpeed;
    public bool playerInRange;
    public GameObject continueButton;
    private Coroutine typingCoroutine;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ResetDialogue();
            }
            else
            {
                StartDialogue();
            }
        }

        if (dialoguePanel.activeInHierarchy && dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        npcNameText.text = gameObject.name;
        npcImage.sprite = npcSprite;
        index = 0;
        
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); 
        }
        dialogueText.text = ""; 
        typingCoroutine = StartCoroutine(Typing());

        continueButton.GetComponent<Button>().onClick.RemoveAllListeners();
        continueButton.GetComponent<Button>().onClick.AddListener(NextLine);
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(Typing());
        }
        else
        {
            ResetDialogue();
        }
    }

    IEnumerator Typing()
    {
        continueButton.SetActive(false);
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void ResetDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = null;
        dialogueText.text = "";
        index = 0;
        continueButton.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            ResetDialogue();
        }
    }
}
