using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Game/Dialogue")]
public class DialogueData : ScriptableObject
{
    public enum DialogueType
    {
        Single,
        Multiple
    }

    public enum AudioMode
    {
        Global,
        Local
    }

    [System.Serializable]
    public class DialogueLine
    {
        [TextArea]
        public string text;



        [Min(0.1f)]
        public float duration = 3f;
    }

    [Header("Settings")]
    public DialogueType dialogueType;

    [Header("Audio")]
    public AudioClip voiceClip;

    public AudioMode audioMode = AudioMode.Global;

    [Header("Single Dialogue")]
    public DialogueLine singleLine;

    [Header("Multiple Dialogue")]
    public List<DialogueLine> multipleLines = new();
}