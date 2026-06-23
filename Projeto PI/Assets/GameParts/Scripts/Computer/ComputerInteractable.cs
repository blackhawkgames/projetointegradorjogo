using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static FirstPersonController;

public class ComputerInteractable : MonoBehaviour, IInteractable
{
    [Header("UI")]
    [SerializeField] private GameObject interactCanvas;

    [Header("Computer")]
    [SerializeField] private ComputerUIManager computerUI;

    [Header("Events")]
    public UnityEvent OnInteract;

    public FloatingIconType GetIconType()
    {
        return FloatingIconType.Computer;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void Start()
    {
        if (interactCanvas != null)
            interactCanvas.SetActive(false);
    }

    public string GetInteractionText()
    {
        return "Usar computador";
    }

    public void Interact()
    {
        if (computerUI != null)
        {
            computerUI.OpenComputer();
        }
        else
        {
            Debug.LogError("ComputerUIManager não atribuído!");
        }

        OnInteract?.Invoke();
    }

    public void ShowUI(string text)
    {
        if (interactCanvas != null)
        {
            interactCanvas.SetActive(true);

            TMP_Text tmp = interactCanvas.GetComponentInChildren<TMP_Text>();

            if (tmp != null)
                tmp.text = text;
        }
    }

    public void HideUI()
    {
        if (interactCanvas != null)
            interactCanvas.SetActive(false);
    }
}