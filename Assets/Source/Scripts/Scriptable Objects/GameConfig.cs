using UnityEngine;

[CreateAssetMenu(menuName = "Config/GameConfig")]
public sealed class GameConfig : ScriptableObject
{
    public LayerMask groundMask;
    public PlayerConfig player;
    public ControllerConfig controller;
    public EnemiesConfig enemies;
    public WeaponsConfig weapons;
}

