using UnityEngine;

[CreateAssetMenu(fileName = "NewChoiceNode", menuName = "DialogueSystem/Choice Node")]
public class DialogueChoiceData : ScriptableObject
{
    [TextArea(3, 5)]
    public string situationText;
    public PlayerChoice[] choices;
}