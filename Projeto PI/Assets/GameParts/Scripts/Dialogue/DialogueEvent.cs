using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueData dialogue;

    [Header("Audio")]
    public AudioSource localAudioSource;

    [Header("Settings")]
    public bool playOnce = true;

    private bool hasPlayed;

    public void PlayDialogue()
    {
        if (playOnce && hasPlayed)
            return;

        DialogueManager.Instance.StartDialogue(dialogue, localAudioSource);

        hasPlayed = true;
    }

    public void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
    }
}