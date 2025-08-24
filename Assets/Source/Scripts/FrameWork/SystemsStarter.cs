using UnityEngine;

public class SystemsStarter : MonoBehaviour
{
    [field: SerializeField] GameConfig gameConfig;
    [field: SerializeField] GameData gameData;
    [field: SerializeField] SaveData saveData;
    [field: SerializeField] UIScreen screen;

    GameSystem[] systems;
    void Start()
    {
        systems = GetComponentsInChildren<GameSystem>();

        saveData = GetComponentInChildren<SaveLoadSystem>().Load();

        foreach (var system in systems)
            system.StartSystem(gameData, saveData, gameConfig, screen);
        foreach (var system in systems)
            system.OnStart();
    }

    void Update()
    {
        foreach (var system in systems)
            system.OnUpdate();
    }
    private void LateUpdate()
    {
        foreach (var system in systems)
            system.OnLateUpdate();
    }
}
