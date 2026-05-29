using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    [Header("Referências Base")]
    public string ChapterName;
    public GameTools gameTools;
    public float fadeDuration = 0.3f;
    public CanvasGroup BackgroundCanvas;
    public ComputerUIManager computerCanvas;

    [Header("Referências para HUD")]
    public TextMeshProUGUI CustomHintText;
    public CanvasGroup HintTextCanvasGroup;
    public string DefaultHintText;
    public float HintFadeDuration = 4f;

    [Header("Referencias para Pause")]
    public GameObject PauseTAB;
    public InputActionReference pauseAction;


    [Header("Referências para Inventário")]
    public GameObject InventoryTab;
    public InputActionReference inventoryAction;



    [Header("Bools Para Check")]
    public bool isPaused = false;
    public bool isInventory = false;



    private Coroutine currentFade;


    private void OnEnable()
    {
        inventoryAction.action.Enable();
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        inventoryAction.action.Disable();
        pauseAction.action.Disable();
    }

    private void Start()
    {
        FadeOut(BackgroundCanvas);
    }

    void OpenInventory()
    {
        gameTools.DisableMovement();
        gameTools.PauseGame();
        
        InventoryTab.SetActive(true);
        isInventory = true;
    }

    void CloseInventory()
    {
        gameTools.EnableMovement();
        gameTools.ResumeGame();
        InventoryTab.SetActive(false);
        isInventory = false;
    }

    private void Update()
    {
        if (inventoryAction.action.WasPressedThisFrame() && !isPaused)
        {
            if(!isInventory && !computerCanvas.isOpen) OpenInventory();
            else CloseInventory();
        }

        if(pauseAction.action.WasPressedThisFrame())
        {
            if (!isPaused)
            {
                OpenPause();
            }
            else ClosePause();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenPause()
    {
        if(isInventory) CloseInventory();
        gameTools.DisableMovement();
        gameTools.PauseGame();
        isPaused = true;
        PauseTAB.SetActive(true);
    }

    public void ClosePause()
    {
        gameTools.EnableMovement();
        gameTools.ResumeGame();
        isPaused = false;
        PauseTAB.SetActive(false);
    }


    public void ShowCustomHint(string hintText)
    {
        CustomHintText.text = hintText;
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(HintRoutine());
    }

    public void ShowDefaultHint()
    {
        CustomHintText.text = DefaultHintText;
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(HintRoutine());
    }

    IEnumerator HintRoutine()
    {
        FadeIn(HintTextCanvasGroup);
        yield return new WaitForSeconds(HintFadeDuration);
        FadeOut(HintTextCanvasGroup);
    }

    public void FadeIn(CanvasGroup targetCanvasGroup)
    {
        StartFade(1f, targetCanvasGroup);
    }

    public void FadeOut(CanvasGroup targetCanvasGroup)
    {
        StartFade(0f, targetCanvasGroup);
    }

    void StartFade(float target, CanvasGroup target1)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeRoutine(target, target1));
    }

    IEnumerator FadeRoutine(float target, CanvasGroup cg)
    {
        float start = cg.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, target, time / fadeDuration);
            yield return null;
        }

        cg.alpha = target;
    }
}
