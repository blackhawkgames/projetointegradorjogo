using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueEvent dialogueEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        dialogueEvent.PlayDialogue();
    }
}