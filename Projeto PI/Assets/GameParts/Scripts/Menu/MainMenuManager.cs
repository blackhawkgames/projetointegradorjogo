using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Configuração Base")]
    public string NewGameScene;
    public GameObject ContinueButton;
    public GameObject CompletionistButton;

    [Header("Configurações Adicionais")]
    public bool NewGameDeleteSaves = false;

    private void Start()
    {
        ContinueButton.SetActive(GameManager.Instance.HasSave);
        CompletionistButton.SetActive(GameManager.Instance.CompletouJogo);
    }

    public void ContinueGame()
    {
        SaveData data = SaveSystem.LoadGame(GameManager.Instance);

        if (data != null)
        {
            GameManager.Instance.ApplySaveData(data);
            LoadingManager.Instance.LoadScene(GameManager.Instance.sceneIndex);
        }
    }

    public void NewGame()
    {
        if (NewGameDeleteSaves)
        {
            SaveSystem.DeleteSave();
        }

        LoadingManager.Instance.LoadScene(NewGameScene);
    }

    public void NewGameDesiredScene(string DesejoScene)
    {
        if (NewGameDeleteSaves)
        {
            SaveSystem.DeleteSave();
        }

        LoadingManager.Instance.LoadScene(DesejoScene);
    }

    public void CompletionistScene()
    {
        LoadingManager.Instance.LoadScene("Tester");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}