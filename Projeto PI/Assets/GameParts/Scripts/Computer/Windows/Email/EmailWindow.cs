using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;

    public EmailManager EmailManager;

    public void OpenBrowser()
    {
        EmailManager.RefreshInbox();
        window.SetActive(true);
    }

    public void CloseBrowser()
    {
        EmailManager.RefreshInbox();
        window.SetActive(false);
    }
}
