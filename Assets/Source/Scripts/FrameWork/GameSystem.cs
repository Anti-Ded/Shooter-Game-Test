using System.Security.Cryptography;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    protected GameData game;
    protected SaveData save;
    protected GameConfig config;
    protected UIScreen screen;
    public void StartSystem(GameData game, SaveData save, GameConfig config, UIScreen screen)
    {
        this.game = game;
        this.save = save;
        this.config = config;
        this.screen = screen;
    }

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnLateUpdate() { }
}
