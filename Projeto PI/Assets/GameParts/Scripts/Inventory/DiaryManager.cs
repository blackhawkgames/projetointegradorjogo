using UnityEngine;
using System.Collections;
using TMPro;

public class DiaryManager : MonoBehaviour
{
    public GameObject DiaryIcon;
    public string DiaryText;
    public TextMeshProUGUI diaryText;

    private void Start()
    {
        diaryText.text = DiaryText;
    }
    public void AddTextOnPage(string text)
    {
        DiaryText = DiaryText + text;
        diaryText.text = DiaryText;
        StartCoroutine(IconRoutine());
    }

    IEnumerator IconRoutine()
    {
        DiaryIcon.SetActive(true);
        yield return new WaitForSeconds(3);
        DiaryIcon.SetActive(false);
    }
}
