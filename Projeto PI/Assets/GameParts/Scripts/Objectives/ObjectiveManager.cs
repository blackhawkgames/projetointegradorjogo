using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [Header("Objectives")]
    public List<ObjectiveData> activeObjectives = new();
    public List<ObjectiveData> completedObjectives = new();

    public Action<ObjectiveData> OnObjectiveAdded;
    public Action<ObjectiveData> OnObjectiveCompleted;

    private MainUIManager mainUIManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        mainUIManager = GetComponent<MainUIManager>();
    }

    public void AddObjective(ObjectiveData objective)
    {
        if (objective == null)
            return;

        if (activeObjectives.Contains(objective))
            return;

        if (completedObjectives.Contains(objective))
            return;

        activeObjectives.Add(objective);

        mainUIManager.ShowCustomHint("Novo Objetivo!");

        Debug.Log($"Novo objetivo: {objective.title}");

        OnObjectiveAdded?.Invoke(objective);
    }

    public void CompleteObjective(ObjectiveData objective)
    {
        if (objective == null)
            return;

        if (!activeObjectives.Contains(objective))
            return;

        activeObjectives.Remove(objective);

        if (!completedObjectives.Contains(objective))
            completedObjectives.Add(objective);

        mainUIManager.ShowCustomHint("Objetivo Concluído!");

        Debug.Log($"Objetivo concluído: {objective.title}");

        OnObjectiveCompleted?.Invoke(objective);
    }

    public void CompleteAndAdd(ObjectiveData completeObj, ObjectiveData newObj)
    {
        CompleteObjective(completeObj);
        AddObjective(newObj);
        StartCoroutine(CompleteAndAddRoutine());
    }

    IEnumerator CompleteAndAddRoutine()
    {
        mainUIManager.ShowCustomHint("Objetivo Concluído!");
        yield return new WaitForSeconds(4);
        mainUIManager.ShowCustomHint("Novo Objetivo!");
    }
}