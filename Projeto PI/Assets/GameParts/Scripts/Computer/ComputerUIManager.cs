using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject computerCanvas;
    [SerializeField] private GameTools gameTools;

    public InputActionReference closeAction;

    public bool isOpen;

    private void OnEnable()
    {
        closeAction.action.Enable();
    }

    private void OnDisable()
    {
        closeAction.action.Disable();
    }

    private void Start()
    {
        computerCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isOpen && closeAction.action.WasPressedThisFrame())
        {
            CloseComputer();
        }
    }

    public void OpenComputer()
    {
        isOpen = true;

        computerCanvas.SetActive(true);

        gameTools.StartCutscene();
        gameTools.DisableMovement();

        Cursor.visible = true;
    }

    public void CloseComputer()
    {
        isOpen = false;

        computerCanvas.SetActive(false);

        gameTools.EndCutscene();
        gameTools.EnableMovement();

        Cursor.visible = false;
    }

    public void CloseComputerFree()
    {
        isOpen = false;

        computerCanvas.SetActive(false);
    }
}