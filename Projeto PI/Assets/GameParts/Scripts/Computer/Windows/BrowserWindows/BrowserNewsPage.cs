using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrowserNewsPage : MonoBehaviour
{
    [Header("Página")]
    public GameObject PageHandler;

    [Header("Variáveis Base")]
    public GameObject NewsPanel;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI ContentText;
    public TextMeshProUGUI HelloText;
    public Image newsImage;
    public CanvasGroup StartingFade;


    [Header("Extras")]
    public float EntranceTime = 3f;
    public string HelloMessage;

    public void OpenNewsPage(string title, string content)
    {
        PageHandler.SetActive(true);
        TitleText.text = title;
        ContentText.text = content;
        HelloText.text = HelloMessage;
        StartCoroutine(LoadingStart());
    }

    IEnumerator LoadingStart()
    {
        float time = 0f;
        while (time < EntranceTime)
        {
            time += Time.deltaTime;
            StartingFade.alpha = Mathf.Lerp(0, 1, time / (EntranceTime/2));
            yield return null;
        }       
        yield return new WaitForSeconds(1f);
        time = 0f;
        while (time < EntranceTime)
        {
            time += Time.deltaTime;
            StartingFade.alpha = Mathf.Lerp(1, 0, time / (EntranceTime / 2));
            yield return null;
        }

        NewsPanel.SetActive(true);
    }

    public void CloseNewsPage()
    {
        StopCoroutine(LoadingStart());
        TitleText.text = "";
        ContentText.text = "";
        NewsPanel.SetActive (false);
        PageHandler.SetActive(false);
    }
}
