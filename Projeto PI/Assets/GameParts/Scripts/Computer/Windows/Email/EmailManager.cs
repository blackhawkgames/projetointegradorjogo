using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class EmailManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private EmailData[] allEmails;
    private List<EmailData> unlockedEmails = new List<EmailData>();
    private HashSet<string> readEmails = new HashSet<string>();

    [Header("Player Stats")]
    [SerializeField] private int riskLevel = 0;
    [SerializeField] private int exposure = 0;

    [Header("Phishing Goals")]
    [SerializeField] private int correctlyIgnoredCount = 0;
    [SerializeField] private int targetIgnoredCount = 5;
    [SerializeField] private UnityEvent onMetaReached;

    [Header("Inbox")]
    [SerializeField] private Transform emailListParent;
    [SerializeField] private EmailButtonUI emailButtonPrefab;

    [Header("Viewer")]
    [SerializeField] private TMP_Text senderText;
    [SerializeField] private TMP_Text subjectText;
    [SerializeField] private TMP_Text bodyText;

    [Header("Interaction (Normal)")]
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private TMP_Text interactionButtonText;
    [SerializeField] private Button interactionButtonComponent;

    [Header("Interaction (Phishing)")]
    [SerializeField] private GameObject phishingOptionsPanel;
    [SerializeField] private Button identifyThreatButton;
    [SerializeField] private Button fallForScamButton;

    [Header("Educational UI")]
    [SerializeField] private GameObject educationalPanel;
    [SerializeField] private TMP_Text educationalText;

    [Header("Reply Events")]
    [SerializeField] private List<EmailReplyEvent> replyEvents;

    private EmailData currentEmail;

    void Start()
    {
        LoadStartingEmails();
        RefreshInbox();

        if (phishingOptionsPanel != null) phishingOptionsPanel.SetActive(false);
        if (educationalPanel != null) educationalPanel.SetActive(false);
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
            EmailButtonUI newButton = Instantiate(emailButtonPrefab, emailListParent);
            newButton.Setup(email, this);
        }
    }

    public void OpenEmail(EmailData email)
    {
        currentEmail = email;

        senderText.text = email.sender;
        subjectText.text = email.subject;
        bodyText.text = email.body;

        if (!readEmails.Contains(email.emailID))
        {
            readEmails.Add(email.emailID);
            riskLevel += 1;
            Debug.Log($"E-mail lido. Nível de Risco atual: {riskLevel}");
        }

        SetupInteraction(email);
    }

    void SetupInteraction(EmailData email)
    {
        interactionButton.SetActive(false);
        if (phishingOptionsPanel != null) phishingOptionsPanel.SetActive(false);

        if (email.isPhishing)
        {
            if (phishingOptionsPanel != null)
            {
                phishingOptionsPanel.SetActive(true);

                identifyThreatButton.onClick.RemoveAllListeners();
                identifyThreatButton.onClick.AddListener(OnIdentifyThreat);

                fallForScamButton.onClick.RemoveAllListeners();
                fallForScamButton.onClick.AddListener(OnFallForScam);
            }
        }
        else if (email.canReply)
        {
            interactionButton.SetActive(true);
            interactionButtonText.text = email.replyButtonText;
            interactionButtonComponent.onClick.RemoveAllListeners();
            interactionButtonComponent.onClick.AddListener(ReplyToCurrentEmail);
        }
    }

    private void OnIdentifyThreat()
    {
        Debug.Log("O jogador identificou a ameaça! Nenhum efeito negativo aplicado.");

        correctlyIgnoredCount++;
        Debug.Log($"Phishing evitado com sucesso! Progresso: {correctlyIgnoredCount}/{targetIgnoredCount}");

        if (correctlyIgnoredCount >= targetIgnoredCount)
        {
            Debug.Log("Meta de e-mails de phishing ignorados alcançada! Invocando evento.");
            onMetaReached?.Invoke();
        }

        RemoveCurrentEmail();
    }

    private void OnFallForScam()
    {
        Debug.Log("O jogador caiu no golpe!");

        exposure += 5;

        if (phishingOptionsPanel != null) phishingOptionsPanel.SetActive(false);

        if (educationalPanel != null && educationalText != null)
        {
            educationalText.text = currentEmail.educationalMessage;
            educationalPanel.SetActive(true);
        }
    }

    public void CloseEducationalPanel()
    {
        if (educationalPanel != null)
        {
            educationalPanel.SetActive(false);
        }
        RemoveCurrentEmail();
    }

    private void RemoveCurrentEmail()
    {
        if (currentEmail != null)
        {
            if (unlockedEmails.Contains(currentEmail))
            {
                unlockedEmails.Remove(currentEmail);
            }
            RefreshInbox();

            senderText.text = "";
            subjectText.text = "";
            bodyText.text = "";

            if (phishingOptionsPanel != null) phishingOptionsPanel.SetActive(false);
            interactionButton.SetActive(false);

            currentEmail = null;
        }
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

    public void AddEmail(EmailData email)
    {
        if (!unlockedEmails.Contains(email))
        {
            unlockedEmails.Add(email);
            RefreshInbox();
        }
    }
}