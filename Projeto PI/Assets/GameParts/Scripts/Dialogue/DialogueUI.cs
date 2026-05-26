using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private void Start()
    {
        dialoguePanel.SetActive(false);

        DialogueManager.Instance.OnDialogueUpdated += UpdateDialogue;
        DialogueManager.Instance.OnDialogueStateChanged += SetDialogueState;
    }

    private void OnDestroy()
    {
        if (DialogueManager.Instance == null)
            return;

        DialogueManager.Instance.OnDialogueUpdated -= UpdateDialogue;
        DialogueManager.Instance.OnDialogueStateChanged -= SetDialogueState;
    }

    private void UpdateDialogue(string text)
    {
        dialogueText.text = text;
    }

    private void SetDialogueState(bool state)
    {
        dialoguePanel.SetActive(state);
    }

    private void Update()
    {
        if (!dialoguePanel.activeSelf)
            return;

    }
}