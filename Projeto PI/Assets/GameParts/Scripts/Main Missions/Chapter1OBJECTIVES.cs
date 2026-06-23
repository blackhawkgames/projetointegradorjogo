using UnityEngine;
using UnityEngine.Events;

public class Chapter1OBJECTIVES : MonoBehaviour
{
    public bool EmailCompleted = false;
    public bool WebConfirmed = false;

    public UnityEvent AfterCompleting;

    public void CompleteEmail()
    {
        EmailCompleted = true;
    }

    public void CompleteWeb()
    {
        WebConfirmed = true;
    }

    public void CheckObjectives()
    {
        if (EmailCompleted && WebConfirmed)
        {
            AfterCompleting?.Invoke();
        }
    }
}
