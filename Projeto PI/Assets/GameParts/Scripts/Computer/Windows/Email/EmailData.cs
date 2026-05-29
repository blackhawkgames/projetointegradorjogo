using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Email", menuName = "Computer/Email")]
public class EmailData : ScriptableObject
{
    [Header("Info")]
    public string emailID;

    public string sender;

    public string subject;

    [TextArea(10, 20)]
    public string body;

    public string date;

    [Header("State")]
    public bool startsUnlocked;

    public bool important;

    [Header("Reply")]
    public bool canReply;

    public string replyButtonText;

    public string replyEventID;
}