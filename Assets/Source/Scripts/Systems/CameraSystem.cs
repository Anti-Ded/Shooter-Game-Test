using UnityEngine;

public class CameraSystem : GameSystem
{
    public override void OnStart()
    {
        game.cameraController = FindFirstObjectByType<CameraControllerComponent>();
    }

    public override void OnLateUpdate()
    {
        Vector3 pos = game.cameraController.transform.position;
        pos = Vector3.Lerp(pos, game.player.transform.position, Time.deltaTime * config.player.cameraLerpSpeed);
        game.cameraController.transform.position = pos;
    }
}
