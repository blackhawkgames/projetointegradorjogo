using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.json";

    public static bool SaveExists()
    {
        return File.Exists(path);
    }

    public static void SaveGame(GameManager gm)
    {
        SaveData data = new SaveData();

        data.estado_mental = gm.estado_mental;
        data.dinheiro = gm.dinheiro;
        data.risco = gm.risco;
        data.exposicao = gm.exposicao;
        data.CompletouJogo = gm.CompletouJogo;
        data.qualidadeGrafica = gm.qualidadeGrafica;
        data.volumeMaster = gm.volumeMaster;
        data.sensibilidadeMouse = gm.sensibilidadeMouse;
        data.chapterSceneIndex = gm.sceneIndex;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Jogo salvo em: " + path);
    }

    public static SaveData LoadGame(GameManager gm)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save não encontrado!");
            return null;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        gm.estado_mental = data.estado_mental;
        gm.dinheiro = data.dinheiro;
        gm.risco = data.risco;
        gm.exposicao = data.exposicao;
        gm.CompletouJogo = data.CompletouJogo;
        gm.qualidadeGrafica = data.qualidadeGrafica;
        gm.volumeMaster = data.volumeMaster;
        gm.sensibilidadeMouse = data.sensibilidadeMouse;
        gm.sceneIndex = data.chapterSceneIndex;

        return data;
    }

    public static void DeleteSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save deletado");
        }
    }
}