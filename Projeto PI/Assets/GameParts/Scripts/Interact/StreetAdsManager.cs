using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StreetAdsManager : MonoBehaviour
{
    [Header("Base Do Sistema")]
    public Image NewsImage;
    public TextMeshProUGUI NewsContent;
    public CanvasGroup NewsCanvasGroup;
    [SerializeField] private GameTools gameTools;

    [Header("Botões de Contexto")]
    public GameObject IgnoreButton;
    public GameObject ContactButton;

    [HideInInspector] public float ExposicaoPorConsequencia = 1f;
    [HideInInspector] public float RiscoPorConsequencia = 1f;
    [HideInInspector] public bool canContact;
    [HideInInspector] public bool isOpen;
    [HideInInspector] public int ID;
    [HideInInspector] public StreetAd currentAd;

    private Coroutine currentLoadingRoutine;

    private void Start()
    {
        NewsContent.text = "";
        NewsCanvasGroup.alpha = 0f;
    }

    private void Update()
    {
        if (!isOpen || Keyboard.current == null) return;

        bool isVisualOnly = currentAd != null && currentAd.IsOnlyVisual;
        if (canContact && !isVisualOnly && (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame))
        {
            ChooseConsequence();
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            CloseNews();
        }
    }

    private void ContextUpdate()
    {
        bool isVisualOnly = currentAd != null && currentAd.IsOnlyVisual;

        if (canContact && !isVisualOnly)
        {
            ContactButton.SetActive(isOpen);
        }
        else
        {
            ContactButton.SetActive(false);
        }

        IgnoreButton.SetActive(isOpen);
    }

    public void OpenNews()
    {
        if (currentLoadingRoutine != null)
        {
            StopCoroutine(currentLoadingRoutine);
        }

        gameTools.StartCutscene();
        gameTools.DisableMovement();

        isOpen = true;
        currentLoadingRoutine = StartCoroutine(LoadingStart());
    }

    public void CloseNews()
    {
        if (currentLoadingRoutine != null)
        {
            StopCoroutine(currentLoadingRoutine);
            currentLoadingRoutine = null;
        }

        NewsCanvasGroup.alpha = 0f;
        gameTools.EndCutscene();
        gameTools.EnableMovement();

        isOpen = false;
        NewsContent.text = "";
        ContextUpdate();
    }

    public void ChooseConsequence()
    {
        if (currentAd != null)
        {
            currentAd.ConsequenciaConcluida();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.exposicao += ExposicaoPorConsequencia;
            GameManager.Instance.risco += RiscoPorConsequencia;
        }
        
        CloseNews();
    }

    IEnumerator LoadingStart()
    {
        float EntranceTime = 2f;
        float time = 0f;
        while (time < EntranceTime)
        {
            time += Time.deltaTime;
            NewsCanvasGroup.alpha = Mathf.Lerp(0, 1, time / (EntranceTime / 2));
            yield return null;
        }

        currentLoadingRoutine = null;
        ContextUpdate();
    }
}