using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryEvent : MonoBehaviour
{
    public DiaryManager dm;
        public string text;
    public bool PlayOnce;
    private bool hasPlayed;

    public void PlayEvent()
    {
        if (PlayOnce)
        {
            if (!hasPlayed)
            {
                dm.AddTextOnPage(text);
                hasPlayed = true;
            }
        }
        else
        {
            dm.AddTextOnPage(text);
        }
    }
}
