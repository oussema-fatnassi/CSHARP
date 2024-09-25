using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCToCharacter : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] string[] dialogue;
    [SerializeField] GameObject continueButton;

    public float wordSpeed = 0.05f;
    public bool playerInRange;

    private int index = 0;
    private bool dialogueActive = false;

    private bool isFollower = false; // Indicateur si le NPC est déjà un follower
    private Transform player; // Référence au leader actuel
    private List<Transform> followers; // Liste des followers actuels

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Supposons que les followers aient un tag "Follower"
        followers = new List<Transform>(GameObject.FindGameObjectsWithTag("Follower").Length);
        foreach (GameObject follower in GameObject.FindGameObjectsWithTag("Follower"))
        {
            followers.Add(follower.transform);
        }
    }

    void Update()
    {
        // Interaction avec le NPC
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueActive)
            {
                NextLine();
            }
            else if (!isFollower)
            {
                StartDialogue();
            }
        }

        // Activer le bouton "Continuer" une fois que le texte est affiché
        if (dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    private void StartDialogue()
    {
        dialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        StartCoroutine(TypeDialogue());
    }

    private void NextLine()
    {
        continueButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeDialogue());
        }
        else
        {
            EndDialogue();
            AddToFollowers(); // Ajouter le NPC au groupe à la fin du dialogue
        }
    }

    private IEnumerator TypeDialogue()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        dialogueText.text = "";
        index = 0;
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
            EndDialogue(); // Fin du dialogue si le joueur s'éloigne
        }
    }

    // Ajouter le NPC à la liste des followers
    private void AddToFollowers()
    {
        if (!isFollower)
        {
            isFollower = true; // Marque le NPC comme follower pour éviter plusieurs ajouts

            // Trouver le dernier follower de la liste
            Transform lastFollower = followers.Count > 0 ? followers[followers.Count - 1] : player;

            // Définir ce NPC comme nouveau follower du dernier personnage
            FollowerController followerController = GetComponent<FollowerController>();
            if (followerController != null)
            {
                followerController.SetLeader(lastFollower); // Le dernier follower devient le leader du NPC
            }

            // Ajouter ce NPC à la liste des followers
            followers.Add(transform);

            // Ajouter le tag "Follower" pour ce NPC
            gameObject.tag = "Follower";
        }
    }
}
