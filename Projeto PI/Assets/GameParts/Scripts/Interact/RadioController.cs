using UnityEngine;

public class RadioController : MonoBehaviour
{
    [Header("Configurações de Áudio")]
    public AudioSource audioSource;
    public AudioClip musicaRadio;

    private bool tocando = false;

    private void Start()
    {
        if (audioSource != null && musicaRadio != null)
        {
            audioSource.clip = musicaRadio;
            audioSource.loop = true;
        }
    }

    public void AlternarRadio()
    {
        if (audioSource == null) return;

        tocando = !tocando;

        if (tocando)
        {
            audioSource.Play();
            Debug.Log("Rádio Ligado!");
        }
        else
        {
            audioSource.Stop();
            Debug.Log("Rádio Desligado!");
        }
    }
}