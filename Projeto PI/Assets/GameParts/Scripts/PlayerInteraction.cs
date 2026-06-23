using UnityEngine;
using UnityEngine.InputSystem;
using static FirstPersonController;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 2.5f;
    public LayerMask interactLayer;
    public Transform cameraTransform;
    public float iconDistance = 3f;
    public InputActionReference interactAction;

    private IInteractable currentInteractable;
    private IInteractable lastInteractable;
    private IInteractable currentIconInteractable;

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

    void CheckFloatingIcons()
    {
        Collider[] nearbyObjects =
            Physics.OverlapSphere(
                transform.position,
                iconDistance,
                interactLayer);

        IInteractable closest = null;

        float closestDistance = Mathf.Infinity;

        foreach (Collider col in nearbyObjects)
        {
            IInteractable interactable = col.GetComponent<IInteractable>();

            if (interactable == null)
                continue;

            float distance =
                Vector3.Distance(transform.position, interactable.GetTransform().position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        if (closest == null)
        {
            if (currentIconInteractable != null)
            {
                FloatingIconManager.Instance.HideIcon();
                currentIconInteractable = null;
            }

            return;
        }

        if (closest != currentIconInteractable)
        {
            currentIconInteractable = closest;

            InteractableObject interactObj =
                closest.GetTransform().GetComponent<InteractableObject>();

            if (interactObj != null)
            {
                FloatingIconManager.Instance.ShowIcon(
                    interactObj.transform,
                    interactObj.iconType,
                    interactObj.iconOffset);
            }
        }
    }

    public void ClearInteraction()
    {
        currentInteractable = null;
        lastInteractable = null;
    }

    void Update()
    {
        CheckInteraction();

        CheckFloatingIcons();

        if (currentInteractable != null &&
            interactAction.action.WasPressedThisFrame())
        {
            currentInteractable.Interact();
        }
    }
}