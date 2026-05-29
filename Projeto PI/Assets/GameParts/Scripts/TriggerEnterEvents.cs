using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvents : MonoBehaviour
{
    public enum TriggerType
    {
        Enter,
        Exit,
        Stay
    }

    [Header("Config")]
    public TriggerType[] triggerTypes;

    public bool oneTimeOnly = false;

    public float stayDelay = 1f;

    [Header("Events")]
    public UnityEvent OnEnter;

    public UnityEvent OnExit;

    public UnityEvent OnStay;

    private bool canInteract = true;

    private bool isCounting;

    bool HasTriggerType(TriggerType type)
    {
        foreach (var trigger in triggerTypes)
        {
            if (trigger == type)
                return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!canInteract)
            return;

        if (!HasTriggerType(TriggerType.Enter))
            return;

        OnEnter?.Invoke();

        if (oneTimeOnly)
            canInteract = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!HasTriggerType(TriggerType.Exit))
            return;

        OnExit?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!canInteract)
            return;

        if (!HasTriggerType(TriggerType.Stay))
            return;

        if (stayDelay <= 0f)
        {
            OnStay?.Invoke();
        }
        else if (!isCounting)
        {
            StartCoroutine(StayCoroutine());
        }
    }

    IEnumerator StayCoroutine()
    {
        isCounting = true;

        yield return new WaitForSeconds(stayDelay);

        OnStay?.Invoke();

        if (oneTimeOnly)
            canInteract = false;

        isCounting = false;
    }
}