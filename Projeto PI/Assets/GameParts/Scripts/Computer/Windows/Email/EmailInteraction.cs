using UnityEngine;
using UnityEngine.Events;

public class EmailInteraction : MonoBehaviour
{
    public UnityEvent OnInteraction;

    public void Execute()
    {
        OnInteraction?.Invoke();
    }
}