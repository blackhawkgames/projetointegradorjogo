using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FakeDownloadButton : MonoBehaviour
{
    [Header("Configurações do Risco")]
    [SerializeField] private float penalidadeExposicao = 40f;
    [SerializeField] private float penalidadeRisco = 40f;
    [SerializeField] private float duracaoDownload = 2.5f;
    [SerializeField] private BrowserMission bm;

    [Header("Componentes de UI do Botão")]
    [SerializeField] private Button botaoDownload;
    [SerializeField] private TMP_Text textoBotao;
    [SerializeField] private TMP_Text textoStatusProgresso;

    [Header("Painel Educativo (Feedback)")]
    [SerializeField] private GameObject painelEducativo;
    [SerializeField] private TMP_Text textoEducativo;
    [TextArea(4, 6)]
    [SerializeField] private string mensagemEducativa = "⚠️ ALERTA DE SEGURANÇA!\n\nVocê acabou de clicar em um botão de download falso.\n\nNa vida real, botões excessivamente chamativos, verdes brilhantes ou que piscam em sites de download costumam camuflar malwares (vírus). Sempre verifique a URL e evite downloads em sites não confiáveis!";

    private bool simulando = false;
    private Coroutine rotinaDownload;
    private string textoOriginalBotao;
    void OnDisable()
    {
        CancelarDownload();
    }
    void Start()
    {
        if (botaoDownload != null)
        {
            botaoDownload.onClick.RemoveAllListeners();
            botaoDownload.onClick.AddListener(IniciarSimulacaoDownload);
            textoOriginalBotao = textoBotao != null ? textoBotao.text : "BAIXAR AGORA";
        }

        if (painelEducativo != null) painelEducativo.SetActive(false);
        if (textoStatusProgresso != null) textoStatusProgresso.text = "";
    }

    public void IniciarSimulacaoDownload()
    {
        if (simulando) return;
        rotinaDownload = StartCoroutine(RotinaDownloadFake());
    }

    private IEnumerator RotinaDownloadFake()
    {
        simulando = true;
        botaoDownload.interactable = false;

        if (textoBotao != null) textoBotao.text = "Baixando...";

        float tempoDecorrido = 0f;
        while (tempoDecorrido < duracaoDownload)
        {
            tempoDecorrido += Time.deltaTime;
            float progresso = Mathf.Clamp01(tempoDecorrido / duracaoDownload) * 100f;

            if (textoStatusProgresso != null)
                textoStatusProgresso.text = $"Progresso: {progresso:F0}%";

            yield return null;
        }

        if (textoStatusProgresso != null) textoStatusProgresso.text = "Download concluído!";
        yield return new WaitForSeconds(0.5f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.exposicao += penalidadeExposicao;
            GameManager.Instance.risco += penalidadeRisco;
            Debug.Log($"+{penalidadeExposicao} de Exposição aplicado via Download Fake! Total: {GameManager.Instance.exposicao}");
        }

        MostrarPainelEducativo();
    }

    public void CancelarDownload()
    {
        if (!simulando) return;

        if (rotinaDownload != null)
        {
            StopCoroutine(rotinaDownload);
            rotinaDownload = null;
        }

        simulando = false;

        if (botaoDownload != null) botaoDownload.interactable = true;
        if (textoBotao != null) textoBotao.text = textoOriginalBotao;
        if (textoStatusProgresso != null) textoStatusProgresso.text = "";
    }

    private void MostrarPainelEducativo()
    {
        if (painelEducativo != null)
        {
            if (textoEducativo != null) textoEducativo.text = mensagemEducativa;
            painelEducativo.SetActive(true);
            bm.onComplete?.Invoke();
        }
    }

    public void FecharPainelEducativo()
    {
        if (painelEducativo != null) painelEducativo.SetActive(false);

        simulando = false;
        botaoDownload.interactable = true;
        if (textoBotao != null) textoBotao.text = textoOriginalBotao;
        if (textoStatusProgresso != null) textoStatusProgresso.text = "";
    }
}