using UnityEngine.Events;

[System.Serializable]
public class EmailReplyEvent
{
    public string replyID;

    public UnityEvent OnReply;
}