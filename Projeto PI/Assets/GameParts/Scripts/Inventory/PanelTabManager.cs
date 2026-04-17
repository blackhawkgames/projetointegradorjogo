using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTabManager : MonoBehaviour
{
    public GameObject FirstTab;
    public List<GameObject> TabList = new List<GameObject>();

    private void OnEnable()
    {
        FirstTab.SetActive(true);
    }


    public void ChangeTab()
    {

    }
}