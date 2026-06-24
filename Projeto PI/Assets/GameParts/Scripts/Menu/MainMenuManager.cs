using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Configuração Base")]
    public string NewGameScene;
    public GameObject ContinueButton;

    [Header("Configurações Adicionais")]
    public bool NewGameDeleteSaves = false;

    private void Start()
    {
        ContinueButton.SetActive(GameManager.Instance.HasSave);
    }

    public void ContinueGame()
    {
        SaveData data = SaveSystem.LoadGame();

        if (data != null)
        {
            GameManager.Instance.ApplySaveData(data);
            LoadingManager.Instance.LoadScene(NewGameScene);
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

    public void ExitGame()
    {
        Application.Quit();
    }
}