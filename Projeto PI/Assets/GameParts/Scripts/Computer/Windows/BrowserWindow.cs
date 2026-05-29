using UnityEngine;

public class BrowserWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;

    public void OpenBrowser()
    {
        window.SetActive(true);
    }

    public void CloseBrowser()
    {
        window.SetActive(false);
    }
}