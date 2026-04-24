using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelTabManager : MonoBehaviour
{
    public MainUIManager mainUIManager;
    public TextMeshProUGUI chapterName;
    public List<GameObject> TabList = new List<GameObject>();

    private void Start()
    {
        chapterName.text = mainUIManager.ChapterName;
    }

    private void OnEnable()
    {
        ChangeTab(0);
    }


    public void ChangeTab(int tab)
    {
        for (int i = 0; i < TabList.Count; i++)
        {
            if (i == tab)
            {
                TabList[i].SetActive(true);
            }
            else
            {
                TabList[i].SetActive(false);
            }
        }
    }
}