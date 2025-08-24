using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemiesSystem : GameSystem
{
    List<EnemyData> enemiesToSpawn = new List<EnemyData>();
    List<EnemyComponent> toRemoveEnemies = new List<EnemyComponent>();

    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {
        SpawnEnemies();

        foreach (var item in game.enemies)
            if (item.Key.position.y > -1) // Check if enemy fall down over ground
                item.Value.Upd();
            else toRemoveEnemies.Add(item.Value);
        RemoveFallenEnemies();
    }

    private void RemoveFallenEnemies()
    {
        if (toRemoveEnemies.Count > 0)
            for (int i = toRemoveEnemies.Count - 1; i >= 0; i--)
                game.pool.Return(toRemoveEnemies[i]);
        toRemoveEnemies.Clear();
    }

    private void SpawnEnemies()
    {
        if (game.enemies.Count < config.enemies.maxEnemiesCount)
        {
            // Get Spawn Point from LevelComponent
            Shuffle(game.level.enemySpawnPoints);
            EnemySpawnPointComponent point = game.level.enemySpawnPoints[0];
            foreach (var item in game.level.enemySpawnPoints)
                if (!item.IsVisible())
                {
                    point = item;
                    break;
                }

            if (enemiesToSpawn.Count == 0)
                ResetList();

            // Get Enemy from Pool
            EnemyData enemyData = enemiesToSpawn[0];
            enemiesToSpawn.RemoveAt(0);
            game.pool.GetEnemy(enemyData.name, point.position);
        }
    }

    void ResetList()
    {
        foreach (var item in config.enemies.list)
            for (int i = 0; i < item.count; i++)
                enemiesToSpawn.Add(item);
        Shuffle(enemiesToSpawn);
    }

    void Shuffle<T>(List<T> List)
    {
        for (int i = List.Count - 1; i >= 1; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            var temp = List[j];
            List[j] = List[i];
            List[i] = temp;
        }
    }
}
