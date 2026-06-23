using UnityEngine;

public class BrowserNewsOpenHandler : MonoBehaviour
{
    [Header("Base")]
    public BrowserNewsPage App;

    [Header("Content")]
    public string Title;
    public string Content;
    public Sprite newImage;

    public void OpenWebsite()
    {
        App.newsImage.sprite = newImage;
        App.OpenNewsPage(Title, Content);
    }
}
