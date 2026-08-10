using UnityEngine;
using UnityEngine.Events;

public class StreetAd : MonoBehaviour
{
    [Header("Base")]
    public StreetAdsManager Manager;
    public MainUIManager MainUIManager;

    [Header("Conteúdo do Anúncio")]
    public Sprite AdImage;
    public string AdText;
    public float AdID;

    [Header("Configurações do Anúncio")]
    public bool IsOnlyVisual;
    public bool CanPlayerInteract = true;
    public float Exposicao = 1f;
    public float Risco = 1f;

    [Header("Interações")]
    public UnityEvent Consequences;

    private bool HasInteracted;

    public void InteragirComAD()
    {
        if (HasInteracted || !CanPlayerInteract)
        {
            MainUIManager.ShowCustomHint("Já vi esse anúncio.");
            return;
        }

        Manager.NewsImage.sprite = AdImage;
        Manager.NewsContent.text = AdText;
        Manager.ExposicaoPorConsequencia = Exposicao;
        Manager.RiscoPorConsequencia = Risco;

        Manager.canContact = IsOnlyVisual ? false : CanPlayerInteract;

        Manager.currentAd = this;
        Manager.OpenNews();
    }

    public void ConsequenciaConcluida()
    {
        if (IsOnlyVisual) return;

        HasInteracted = true;
        CanPlayerInteract = false;
        Consequences?.Invoke();
    }
}