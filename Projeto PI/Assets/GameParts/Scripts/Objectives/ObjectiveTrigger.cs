using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public enum ObjectiveAction
    {
        Add,
        Complete,
        CompleteAndAdd
    }

    [Header("Action")]
    public ObjectiveAction action;

    [Header("Objectives")]
    public ObjectiveData objective;
    public ObjectiveData newObjective;

    [Header("Settings")]
    public bool triggerOnce = true;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered && triggerOnce)
            return;

        if (!other.CompareTag("Player"))
            return;

        Execute();

        triggered = true;
    }

    public void Execute()
    {
        switch (action)
        {
            case ObjectiveAction.Add:
                ObjectiveManager.Instance.AddObjective(objective);
                break;

            case ObjectiveAction.Complete:
                ObjectiveManager.Instance.CompleteObjective(objective);
                break;

            case ObjectiveAction.CompleteAndAdd:
                ObjectiveManager.Instance.CompleteAndAdd(objective, newObjective);
                break;
        }
    }
}