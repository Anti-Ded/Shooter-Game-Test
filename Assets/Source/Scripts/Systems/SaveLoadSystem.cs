using System.IO;
using UnityEngine;

public class SaveLoadSystem : GameSystem
{
    bool inited;
    public override void OnStart()
    {
        game.saveLoad = this;
        inited = true;
    }
    private void OnDestroy()
    {
        Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (inited)
            Save();
    }
    public void Save()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gamesave.json");
        File.WriteAllText(filePath, JsonUtility.ToJson(save));
        Debug.Log("Game saved");
    }
    public SaveData Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gamesave.json");
        if (!File.Exists(filePath))
        {
            Debug.Log("No savegame file");
            return new SaveData();
        }
        string json = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("No savegame data found");
            return new SaveData();
        }
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        if (data == null)
        {
            Debug.Log("Savegame has no data");
            return new SaveData();
        }
        Debug.Log("Savegame loaded");
        return data;
    }
}
