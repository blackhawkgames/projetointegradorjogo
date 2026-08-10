using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance;

    // Estrutura para vincular um ID de texto ao UnityEvent na Cena
    [System.Serializable]
    public struct ChoiceSceneEvent
    {
        public string eventId;
        public UnityEvent onEventTriggered;
    }

    [Header("UI Elements")]
    public GameObject choicePanel;
    public TextMeshProUGUI situationTextUI;
    public Transform choicesContainer;
    public GameObject choiceItemPrefab;

    [Header("Referências da Cena")]
    public GameTools gameTools;
    public MainUIManager mainUIManager;

    [Header("Eventos da Cena (Mapeados por ID)")]
    public List<ChoiceSceneEvent> sceneEvents = new List<ChoiceSceneEvent>();

    private DialogueChoiceData currentData;
    private bool isWaitingForChoice = false;
    private List<GameObject> activeChoiceItems = new List<GameObject>();

    private readonly Key[] digitKeys = {
        Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4, Key.Digit5,
        Key.Digit6, Key.Digit7, Key.Digit8, Key.Digit9
    };

    private readonly Key[] numpadKeys = {
        Key.Numpad1, Key.Numpad2, Key.Numpad3, Key.Numpad4, Key.Numpad5,
        Key.Numpad6, Key.Numpad7, Key.Numpad8, Key.Numpad9
    };

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartChoice(DialogueChoiceData data)
    {
        if (data == null || data.choices == null || data.choices.Length == 0) return;

        currentData = data;
        situationTextUI.text = data.situationText;

        ClearChoices();

        for (int i = 0; i < data.choices.Length; i++)
        {
            GameObject itemObj = Instantiate(choiceItemPrefab, choicesContainer);
            activeChoiceItems.Add(itemObj);

            ChoiceUIItem choiceItem = itemObj.GetComponent<ChoiceUIItem>();
            if (choiceItem != null)
            {
                choiceItem.Setup(i, data.choices[i].choiceText, MakeChoice);
            }
        }

        choicePanel.SetActive(true);
        isWaitingForChoice = true;

        if (gameTools != null)
        {
            gameTools.DisableMovement();
            gameTools.StartCutscene();
        }
    }

    private void Update()
    {
        if (!isWaitingForChoice || currentData == null || Keyboard.current == null) return;

        int choiceCount = currentData.choices.Length;

        for (int i = 0; i < choiceCount && i < digitKeys.Length; i++)
        {
            if (Keyboard.current[digitKeys[i]].wasPressedThisFrame ||
                Keyboard.current[numpadKeys[i]].wasPressedThisFrame)
            {
                MakeChoice(i);
                break;
            }
        }
    }

    public void MakeChoice(int index)
    {
        if (!isWaitingForChoice || index < 0 || index >= currentData.choices.Length) return;

        isWaitingForChoice = false;
        choicePanel.SetActive(false);

        PlayerChoice selectedChoice = currentData.choices[index];

        // 1. Aplica consequências de variáveis
        if (GameManager.Instance != null)
        {
            GameManager.Instance.exposicao += selectedChoice.exposicaoPorConsequencia;
            GameManager.Instance.risco += selectedChoice.riscoPorConsequencia;
        }

        // 2. Exibe mensagem educativa (se houver)
        if (!string.IsNullOrEmpty(selectedChoice.mensagemEducativa) && mainUIManager != null)
        {
            mainUIManager.ShowCustomHint(selectedChoice.mensagemEducativa);
        }

        // 3. Dispara o evento de cena por ID se 'hasEvent' estiver ativo
        if (selectedChoice.hasEvent && !string.IsNullOrEmpty(selectedChoice.eventId))
        {
            TriggerSceneEvent(selectedChoice.eventId);
        }

        ClearChoices();

        // 4. Próxima etapa
        if (selectedChoice.nextDialogueNode != null)
        {
            StartChoice(selectedChoice.nextDialogueNode);
        }
        else
        {
            if (gameTools != null)
            {
                gameTools.EnableMovement();
                gameTools.EndCutscene();
            }
        }
    }

    // Busca o evento pelo ID na lista e o executa
    private void TriggerSceneEvent(string id)
    {
        foreach (var sceneEvent in sceneEvents)
        {
            if (sceneEvent.eventId == id)
            {
                sceneEvent.onEventTriggered?.Invoke();
                return;
            }
        }

        Debug.LogWarning($"[ChoiceManager] Nenhum evento de cena foi encontrado para o ID: '{id}'");
    }

    private void ClearChoices()
    {
        foreach (GameObject item in activeChoiceItems)
        {
            Destroy(item);
        }
        activeChoiceItems.Clear();
    }
}