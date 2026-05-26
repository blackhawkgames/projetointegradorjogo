using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [Header("List")]
    public Transform objectiveContainer;
    public GameObject objectiveButtonPrefab;

    [Header("Description")]
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    private Dictionary<ObjectiveData, GameObject> objectiveButtons = new();

    private void Start()
    {
        ObjectiveManager.Instance.OnObjectiveAdded += AddObjectiveUI;
        ObjectiveManager.Instance.OnObjectiveCompleted += RemoveObjectiveUI;

        foreach (ObjectiveData objective in ObjectiveManager.Instance.activeObjectives)
        {
            AddObjectiveUI(objective);
        }

        if (ObjectiveManager.Instance.activeObjectives.Count > 0)
        {
            ShowObjective(ObjectiveManager.Instance.activeObjectives[0]);
        }
    }

    private void OnDestroy()
    {
        if (ObjectiveManager.Instance == null)
            return;

        ObjectiveManager.Instance.OnObjectiveAdded -= AddObjectiveUI;
        ObjectiveManager.Instance.OnObjectiveCompleted -= RemoveObjectiveUI;
    }

    private void AddObjectiveUI(ObjectiveData objective)
    {
        if (objectiveButtons.ContainsKey(objective))
            return;

        GameObject go = Instantiate(objectiveButtonPrefab, objectiveContainer);

        ObjectiveButtonUI buttonUI = go.GetComponent<ObjectiveButtonUI>();

        buttonUI.Setup(objective, this);

        objectiveButtons.Add(objective, go);

        if (objectiveButtons.Count == 1)
        {
            ShowObjective(objective);
        }
    }

    private void RemoveObjectiveUI(ObjectiveData objective)
    {
        if (!objectiveButtons.ContainsKey(objective))
            return;

        GameObject button = objectiveButtons[objective];

        objectiveButtons.Remove(objective);

        Destroy(button);

        if (titleText.text == objective.title)
        {
            titleText.text = "";
            descriptionText.text = "";

            foreach (var obj in objectiveButtons.Keys)
            {
                ShowObjective(obj);
                break;
            }
        }
    }

    public void ShowObjective(ObjectiveData objective)
    {
        titleText.text = objective.title;
        descriptionText.text = objective.description;
    }
}