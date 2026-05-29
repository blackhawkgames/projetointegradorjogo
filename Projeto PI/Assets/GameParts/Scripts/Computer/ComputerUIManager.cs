using UnityEngine;

public class ComputerUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject computerCanvas;
    [SerializeField] private GameTools gameTools;

    public bool isOpen;

    private void Start()
    {
        computerCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
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