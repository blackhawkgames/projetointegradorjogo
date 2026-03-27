using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.json";

    // Verifica se existe save
    public static bool SaveExists()
    {
        return File.Exists(path);
    }

    // Salvar
    public static void SaveGame(GameManager gm)
    {
        SaveData data = new SaveData();

        data.estado_mental = gm.estado_mental;
        data.dinheiro = gm.dinheiro;
        data.risco = gm.risco;
        data.exposicao = gm.exposicao;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Jogo salvo em: " + path);
    }

    // Carregar
    public static SaveData LoadGame()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save não encontrado!");
            return null;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        return data;
    }

    // Deletar save
    public static void DeleteSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save deletado");
        }
    }
}