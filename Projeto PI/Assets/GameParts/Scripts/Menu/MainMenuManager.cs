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
        
    }
    public void ContinueGame()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(NewGameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
