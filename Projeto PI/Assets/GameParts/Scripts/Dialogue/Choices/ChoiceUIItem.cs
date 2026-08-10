using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ChoiceUIItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI choiceText;
    [SerializeField] private Button choiceButton;

    public void Setup(int index, string text, Action<int> onClickCallback)
    {
        choiceText.text = $"{index + 1}. {text}";

        choiceButton.onClick.RemoveAllListeners();
        choiceButton.onClick.AddListener(() => onClickCallback?.Invoke(index));
    }
}