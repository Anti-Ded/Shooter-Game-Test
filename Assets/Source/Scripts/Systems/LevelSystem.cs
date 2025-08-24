using UnityEngine;

public class LevelSystem : GameSystem
{
    Vector3 pos;
    public override void OnStart()
    {
        game.level = FindFirstObjectByType<LevelComponent>();
    }

    public override void OnUpdate()
    {
    /* This Was inifinity borderless system... No interest
      pos = game.player.transform.position;
        pos.y = 0;
        pos.x = Mathf.RoundToInt(pos.x / 25f) * 25f;
        pos.z = Mathf.RoundToInt(pos.z / 25f) * 25f;
        game.level.transform.position = pos;*/
    }
}
