using System;
using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Action<string> OnDialogueUpdated;
    public Action<bool> OnDialogueStateChanged;

    [Header("Global Audio")]
    public AudioSource globalAudioSource;

    private DialogueData currentDialogue;

    private int currentIndex;
    private bool dialogueActive;

    private AudioSource localAudioSource;

    private Coroutine dialogueRoutine;

    public bool IsDialogueActive => dialogueActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(DialogueData dialogue, AudioSource localSource = null)
    {
        if (dialogue == null)
            return;

        if (dialogueActive)
        {
            EndDialogue();
        }

        currentDialogue = dialogue;

        localAudioSource = localSource;

        currentIndex = 0;
        dialogueActive = true;

        OnDialogueStateChanged?.Invoke(true);

        PlayVoice();

        dialogueRoutine = StartCoroutine(DialogueRoutine());
    }

    private IEnumerator DialogueRoutine()
    {
        // SINGLE
        if (currentDialogue.dialogueType == DialogueData.DialogueType.Single)
        {
            DialogueData.DialogueLine line =
                currentDialogue.singleLine;

            OnDialogueUpdated?.Invoke(line.text);

            yield return new WaitForSeconds(line.duration);

            EndDialogue();
        }

        // MULTIPLE
        else
        {
            while (currentIndex < currentDialogue.multipleLines.Count)
            {
                DialogueData.DialogueLine line =
                    currentDialogue.multipleLines[currentIndex];

                OnDialogueUpdated?.Invoke(line.text);

                yield return new WaitForSeconds(line.duration);

                currentIndex++;
            }

            EndDialogue();
        }
    }

    private void PlayVoice()
    {
        if (currentDialogue.voiceClip == null)
            return;

        AudioSource source = null;

        switch (currentDialogue.audioMode)
        {
            case DialogueData.AudioMode.Global:
                source = globalAudioSource;
                break;

            case DialogueData.AudioMode.Local:
                source = localAudioSource;
                break;
        }

        if (source == null)
            return;

        source.Stop();
        source.clip = currentDialogue.voiceClip;
        source.Play();
    }

    public void EndDialogue()
    {
        if (!dialogueActive)
            return;

        dialogueActive = false;

        if (dialogueRoutine != null)
        {
            StopCoroutine(dialogueRoutine);
        }

        OnDialogueUpdated?.Invoke("");
        OnDialogueStateChanged?.Invoke(false);

        currentDialogue = null;
    }
}