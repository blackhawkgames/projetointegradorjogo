using UnityEngine;
using UnityEngine.Events;

public class EventOnStart : MonoBehaviour
{
    public UnityEvent event1;
    void Start()
    {
        event1?.Invoke();
    }
}
