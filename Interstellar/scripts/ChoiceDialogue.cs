using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChoiceDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueStage
    {
        public string question1; // Text for Choice 1
        public string question2; // Text for Choice 2
        public string response1; // Response to Choice 1
        public string response2; // Response to Choice 2
    }

    public Button interactButton; // Initial interact button
    public GameObject choicesGroup; // Parent GameObject for choice buttons
    public Button choice1Button; // Choice 1 button
    public Button choice2Button; // Choice 2 button
    public TextMeshProUGUI choice1Text; // Text component of Choice 1
    public TextMeshProUGUI choice2Text; // Text component of Choice 2
    public TextMeshProUGUI dialogueText; // Dialogue text displayed in response
    public GameObject dialoguePanel; // Dialogue panel GameObject
    public Transform character; // Character transform for positioning dialogue

    public List<DialogueStage> dialogueStages; // List of dialogues
    private int currentStageIndex = 0; // Current stage in the dialogue sequence

    private void Start()
    {
        // Hide initial UI elements
        choicesGroup.SetActive(false);
        dialoguePanel.SetActive(false);

        // Add listener for interact button
        interactButton.onClick.AddListener(OnInteractClicked);

        // Add listeners for choice buttons
        choice1Button.onClick.AddListener(() => OnChoiceSelected(1));
        choice2Button.onClick.AddListener(() => OnChoiceSelected(2));
    }

    private void OnInteractClicked()
    {
        // Hide the interact button and show the first set of choices
        interactButton.gameObject.SetActive(false);
        choicesGroup.SetActive(true);

        // Load the first stage
        LoadStage();
    }

    private void LoadStage()
    {
        if (currentStageIndex < dialogueStages.Count)
        {
            DialogueStage currentStage = dialogueStages[currentStageIndex];

            // Update choice button texts
            choice1Text.text = currentStage.question1;
            choice2Text.text = currentStage.question2;
        }
        else
        {
            // End of the dialogue sequence
            EndDialogue();
        }
    }

    private void OnChoiceSelected(int choice)
    {
        DialogueStage currentStage = dialogueStages[currentStageIndex];

        // Display the response based on the selected choice
        dialoguePanel.SetActive(true);
        if (choice == 1)
        {
            dialogueText.text = currentStage.response1;
        }
        else if (choice == 2)
        {
            dialogueText.text = currentStage.response2;
        }

        // Hide the choices and move to the next stage after a delay
        choicesGroup.SetActive(false);
        Invoke(nameof(NextStage), 2.0f); // Delay for response display
    }

    private void NextStage()
    {
        // Hide the dialogue panel
        dialoguePanel.SetActive(false);

        // Move to the next stage and reload choices
        currentStageIndex++;
        LoadStage();

        // Show choices if not at the end
        if (currentStageIndex < dialogueStages.Count)
        {
            choicesGroup.SetActive(true);
        }
    }

    private void EndDialogue()
    {
        // Dialogue sequence completed
        dialogueText.text = "End of conversation.";
        dialoguePanel.SetActive(true);
        // Optionally, disable the choice buttons
        choicesGroup.SetActive(false);
    }
}
