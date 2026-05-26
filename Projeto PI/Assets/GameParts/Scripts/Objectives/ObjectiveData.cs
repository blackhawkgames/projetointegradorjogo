using UnityEngine;

[CreateAssetMenu(fileName = "New Objective", menuName = "Game/Objectives/Objective")]
public class ObjectiveData : ScriptableObject
{
    [Header("Info")]
    public string objectiveID;

    [TextArea]
    public string title;

    [TextArea]
    public string description;
}