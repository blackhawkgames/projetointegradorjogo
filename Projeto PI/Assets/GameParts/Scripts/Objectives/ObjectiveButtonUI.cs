using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveButtonUI : MonoBehaviour
{
    public Button button;
    public TMP_Text titleText;

    private ObjectiveData objectiveData;
    private ObjectiveUI objectiveUI;

    public void Setup(ObjectiveData data, ObjectiveUI ui)
    {
        objectiveData = data;
        objectiveUI = ui;

        titleText.text = data.title;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        objectiveUI.ShowObjective(objectiveData);
    }
}