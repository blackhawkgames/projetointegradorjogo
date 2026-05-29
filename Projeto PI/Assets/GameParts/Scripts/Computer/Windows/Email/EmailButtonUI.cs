using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailButtonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text senderText;
    [SerializeField] private TMP_Text subjectText;
    [SerializeField] private Button button;

    private EmailData emailData;
    private EmailManager manager;

    public void Setup(EmailData data, EmailManager emailManager)
    {
        emailData = data;
        manager = emailManager;

        senderText.text = data.sender;
        subjectText.text = data.subject;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OpenEmail);
    }

    void OpenEmail()
    {
        manager.OpenEmail(emailData);
    }
}