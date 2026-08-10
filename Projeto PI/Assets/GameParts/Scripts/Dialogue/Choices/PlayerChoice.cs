using UnityEngine;

[System.Serializable]
public class PlayerChoice
{
    [TextArea(1, 3)]
    public string choiceText;

    [Header("Consequências da Escolha")]
    public float riscoPorConsequencia;
    public float exposicaoPorConsequencia;

    [Header("Feedback ao Jogador")]
    [TextArea(2, 4)]
    public string mensagemEducativa;

    [Header("Eventos de Cena (via ID)")]
    public bool hasEvent = false;
    public string eventId;

    [Header("Fluxo da Conversa")]
    public DialogueChoiceData nextDialogueNode;
}