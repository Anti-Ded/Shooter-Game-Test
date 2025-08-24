using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/EnemiesConfig")]
public sealed class EnemiesConfig : ScriptableObject
{
    public EnemyHealthBarFrame healthBarFrame;
    public int maxEnemiesCount;
    public string nameOfEnemyThatMediumDrops;
    public float dropPower = 2;
    public float dropTime = 2;
    public EnemyData[] list;
}

[Serializable] public class EnemyData
{
    public string name;
    public float speed;
    public float health;
    public float playerHitDamage;
    public int count;
    public EnemyComponent prefab;
}

