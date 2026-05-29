using UnityEngine;
using UnityEngine.InputSystem;
using static FirstPersonController;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 2.5f;
    public LayerMask interactLayer;
    public Transform cameraTransform;

    public InputActionReference interactAction;

    private IInteractable currentInteractable;
    private IInteractable lastInteractable;

    void CheckInteraction()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (lastInteractable != interactable)
                {
                    if (lastInteractable != null)
                    {
                        MonoBehaviour mb = lastInteractable as MonoBehaviour;

                        if (mb != null)
                            lastInteractable.HideUI();
                    }

                    interactable.ShowUI(interactable.GetInteractionText());

                    lastInteractable = interactable;
                }

                currentInteractable = interactable;
                return;
            }
        }

        if (lastInteractable != null)
        {
            MonoBehaviour mb = lastInteractable as MonoBehaviour;

            if (mb != null)
            {
                lastInteractable.HideUI();
            }

            lastInteractable = null;
        }

        currentInteractable = null;
    }

    void Start()
    {
        interactAction.action.Enable();
    }

    public void ClearInteraction()
    {
        currentInteractable = null;
        lastInteractable = null;
    }

    void Update()
    {
        CheckInteraction();

        if (currentInteractable != null &&
            interactAction.action.WasPressedThisFrame())
        {
            currentInteractable.Interact();
        }
    }
}