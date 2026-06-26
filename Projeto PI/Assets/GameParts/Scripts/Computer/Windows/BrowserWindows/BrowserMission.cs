using UnityEngine;
using UnityEngine.Events;

public class BrowserMission : MonoBehaviour
{
    public int necessaryCount = 5;
    private int objectiveCount = 0;
    public UnityEvent onComplete;

    public void IncreaseCount()
    {
        objectiveCount++;
        CheckObjective();
    }

    void CheckObjective()
    {
        if(objectiveCount >= necessaryCount) 
        { 
            onComplete?.Invoke();
        }
    }
}
