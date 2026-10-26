using UnityEngine;
using UnityEngine.Events;

public class C2ObjectivesSync : MonoBehaviour
{
    public UnityEvent CompleteCountEvent;
    public UnityEvent CompleteHalfEvent;

    public int adsCount = 0;

    public int adsLimit = 5;

    private bool HalfDone = false;

    private void VerifyCount()
    {
        if(adsCount > 4 && !HalfDone)
        {
            CompleteHalfEvent?.Invoke();
            HalfDone = true;
        }

        if(adsCount >= adsLimit)
        {
            CompleteCountEvent?.Invoke();
        }
    }


    public void IncreaseAdsCount()
    {
        adsCount++;
        VerifyCount();
    }
}
