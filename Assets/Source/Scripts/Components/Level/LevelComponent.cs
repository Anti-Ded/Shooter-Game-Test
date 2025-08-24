using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    public List<EnemySpawnPointComponent> enemySpawnPoints;

    public void Calculate()
    {
        enemySpawnPoints = GetComponentsInChildren<EnemySpawnPointComponent>().ToList();
        foreach (var item in enemySpawnPoints)
            item.Calculate();
    }
}
