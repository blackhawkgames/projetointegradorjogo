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

    public FloatingIconType iconType;

    public FloatingIconType GetIconType()
    {
        return iconType;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    [Header("Floating Icon")]
    public Vector3 iconOffset = new Vector3(0f, 1.5f, 0f);

    private void Start()
    {
        if (interactCanvas != null)
            interactCanvas.SetActive(false);
    }

    public void ShowUI(string text)
    {
        if (interactCanvas == null)
            return;

        interactCanvas.SetActive(true);

        var tmp = interactCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if (tmp != null)
            tmp.text = text;
    }

    public void HideUI()
    {
        if (interactCanvas != null)
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
        HideUI();

        PlayerInteraction interaction =
            FindFirstObjectByType<PlayerInteraction>();

        if (interaction != null)
            interaction.ClearInteraction();

        FloatingIconManager.Instance.RemoveTarget(transform);

        Debug.Log("Item coletado");

        Destroy(gameObject);
    }


    void Use()
    {
        Debug.Log("Objeto usado");
    }

    private void OnDestroy()
    {
        PlayerInteraction interaction =
            FindFirstObjectByType<PlayerInteraction>();

        if (interaction != null)
            interaction.ClearInteraction();
    }
}