using UnityEngine;
using UnityEngine.Events;
using static FirstPersonController;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public enum InteractionType
    {
        Comment,
        Collect,
        Use
    }

    public InteractionType interactionType;

    [TextArea]
    public string commentText;

    public GameObject interactCanvas;

    public UnityEvent OnInteract;

    private void Start()
    {
        if (interactCanvas != null)
            interactCanvas.SetActive(false);
    }

    public void ShowUI(string text)
    {
        interactCanvas.SetActive(true);

        var tmp = interactCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        tmp.text = text;
    }

    public void HideUI()
    {
        interactCanvas.SetActive(false);
    }

    public string GetInteractionText()
    {
        switch (interactionType)
        {
            case InteractionType.Comment:
                return "Examinar";
            case InteractionType.Collect:
                return "Coletar";
            case InteractionType.Use:
                return "Usar";
        }

        return "";
    }

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Comment:
                Debug.Log(commentText);
                break;

            case InteractionType.Collect:
                Collect();
                break;

            case InteractionType.Use:
                Use();
                break;
        }

        OnInteract?.Invoke();
    }

    void Collect()
    {
        Debug.Log("Item coletado");
        Destroy(gameObject);
    }

    void Use()
    {
        Debug.Log("Objeto usado");
    }
}