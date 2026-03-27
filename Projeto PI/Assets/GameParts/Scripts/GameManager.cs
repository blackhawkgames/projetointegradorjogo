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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Aqui só verifica se existe save
            HasSave = SaveSystem.SaveExists();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Aplicar dados carregados
    public void ApplySaveData(SaveData data)
    {
        estado_mental = data.estado_mental;
        dinheiro = data.dinheiro;
        risco = data.risco;
        exposicao = data.exposicao;
    }

    // Salvar
    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }
}