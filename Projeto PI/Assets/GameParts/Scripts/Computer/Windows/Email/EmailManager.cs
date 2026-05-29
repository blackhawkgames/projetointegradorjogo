using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private EmailData[] allEmails;

    private List<EmailData> unlockedEmails =
        new List<EmailData>();

    [Header("Inbox")]
    [SerializeField] private Transform emailListParent;

    [SerializeField] private EmailButtonUI emailButtonPrefab;

    [Header("Viewer")]
    [SerializeField] private TMP_Text senderText;

    [SerializeField] private TMP_Text subjectText;

    [SerializeField] private TMP_Text bodyText;

    [Header("Interaction")]
    [SerializeField] private GameObject interactionButton;

    [SerializeField] private TMP_Text interactionButtonText;

    [SerializeField] private Button interactionButtonComponent;

    [Header("Reply Events")]
    [SerializeField]
    private List<EmailReplyEvent> replyEvents;

    private EmailData currentEmail;

    void Start()
    {
        LoadStartingEmails();

        RefreshInbox();
    }

    void LoadStartingEmails()
    {
        foreach (var email in allEmails)
        {
            if (email.startsUnlocked)
            {
                unlockedEmails.Add(email);
            }
        }
    }

    public void RefreshInbox()
    {
        foreach (Transform child in emailListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var email in unlockedEmails)
        {
            EmailButtonUI newButton =
                Instantiate(emailButtonPrefab,
                emailListParent);

            newButton.Setup(email, this);
        }
    }

    public void OpenEmail(EmailData email)
    {
        currentEmail = email;

        senderText.text = email.sender;
        subjectText.text = email.subject;
        bodyText.text = email.body;

        SetupInteraction(email);
    }

    public void UnlockEmail(string id)
    {
        foreach (var email in allEmails)
        {
            if (email.emailID == id)
            {
                if (!unlockedEmails.Contains(email))
                {
                    unlockedEmails.Add(email);
                    Debug.Log("Unlocking: " + id);
                    RefreshInbox();
                }
                return;
            }
        }
    }

    public void ReplyToCurrentEmail()
    {
        if (currentEmail == null)
            return;

        ExecuteReplyEvent(currentEmail.replyEventID);

    }

    public void ExecuteReplyEvent(string id)
    {
        foreach (var reply in replyEvents)
        {
            if (reply.replyID == id)
            {
                reply.OnReply?.Invoke();
                interactionButton.SetActive(false);
                return;
            }
        }

        Debug.LogWarning("Reply Event não encontrado: " + id);
    }

    void SetupInteraction(EmailData email)
    {
        if (email.canReply)
        {
            interactionButton.SetActive(true);

            interactionButtonText.text =
                email.replyButtonText;

            interactionButtonComponent.onClick
                .RemoveAllListeners();

            interactionButtonComponent.onClick
                .AddListener(ReplyToCurrentEmail);
        }
        else
        {
            interactionButton.SetActive(false);
        }
    }

    public void AddEmail(EmailData email)
    {
        if (!unlockedEmails.Contains(email))
        {
            unlockedEmails.Add(email);

            RefreshInbox();
        }
    }
}