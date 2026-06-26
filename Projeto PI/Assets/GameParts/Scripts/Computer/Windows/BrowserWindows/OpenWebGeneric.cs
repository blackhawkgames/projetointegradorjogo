using UnityEngine;

public class OpenWebGeneric : MonoBehaviour
{
    public bool HasOpened = false;
    public BrowserMission browserMission;

    public void ClosePage(GameObject page)
    {
        page.SetActive(false);
        givePoints();
    }

    void givePoints()
    {
        if(!HasOpened)
        {
            browserMission.IncreaseCount();
        }
        HasOpened = true;
    }
}
