using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Informações Básicas")]
    public bool HasSave = false;

    [Header("Infos Player")]
    public int estado_mental = 0;
    public float dinheiro = 0f;
    public float risco = 0f;
    public float exposicao = 0f;
    public bool CompletouJogo = false;

    [Header("Configurações do Jogo")]
    public int qualidadeGrafica = 2;
    public float volumeMaster = 1f;
    public float sensibilidadeMouse = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            HasSave = SaveSystem.SaveExists();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplySaveData(SaveData data)
    {
        estado_mental = data.estado_mental;
        dinheiro = data.dinheiro;
        risco = data.risco;
        exposicao = data.exposicao;
        CompletouJogo = data.CompletouJogo;
        qualidadeGrafica = data.qualidadeGrafica;
        volumeMaster = (data.volumeMaster == 0f && data.sensibilidadeMouse == 0f) ? 1f : data.volumeMaster;
        sensibilidadeMouse = data.sensibilidadeMouse == 0f ? 2f : data.sensibilidadeMouse;

        ApplySettings();
    }

    public void ApplySettings()
    {
        QualitySettings.SetQualityLevel(qualidadeGrafica);
        AudioListener.volume = volumeMaster;

        FirstPersonController player = FindAnyObjectByType<FirstPersonController>();
        if (player != null)
        {
            player.mouseSensitivity = sensibilidadeMouse;
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }
}