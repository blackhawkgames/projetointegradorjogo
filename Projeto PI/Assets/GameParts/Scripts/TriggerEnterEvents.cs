using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvents : MonoBehaviour
{
    [Header("Configurações Básicas")]
    public bool oneTimeOnly = false;
    public float StayTime = 1f;

    [Header("Eventos")]
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    public UnityEvent TriggerStay;

    private bool CanInteract = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && CanInteract)
        {
            TriggerEnter?.Invoke();
            if(oneTimeOnly) CanInteract = false;
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && CanInteract)
        {
            TriggerExit?.Invoke();
            if (oneTimeOnly) CanInteract = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && CanInteract)
        {
            if (StayTime < 0f)
            {
                TriggerStay?.Invoke();
            }
            else
            {
                StartCoroutine(CountdownTime());
            }
        }
    }

    IEnumerator CountdownTime()
    {
        TriggerStay?.Invoke();
        yield return new WaitForSeconds(StayTime);
        CanInteract = false;
    }
}
