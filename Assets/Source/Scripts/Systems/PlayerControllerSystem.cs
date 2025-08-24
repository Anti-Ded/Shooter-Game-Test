using UnityEngine;

public class PlayerControllerSystem : GameSystem
{
    Vector3 moveVector;
    public override void OnStart()
    {
        game.player = FindFirstObjectByType<PlayerComponent>();
        game.player.Prestart(game, config.player);
    }

    public override void OnUpdate()
    {
        foreach (var keyboard in config.controller.keyboards)
        {
            if (Input.GetKeyDown(keyboard.up) || Input.GetKeyUp(keyboard.Down))
                moveVector += Vector3.forward;
            if (Input.GetKeyUp(keyboard.up) || Input.GetKeyDown(keyboard.Down))
                moveVector -= Vector3.forward;

            if (Input.GetKeyDown(keyboard.right) || Input.GetKeyUp(keyboard.left))
                moveVector += Vector3.right;
            if (Input.GetKeyUp(keyboard.right) || Input.GetKeyDown(keyboard.left))
                moveVector -= Vector3.right;
        }

        game.player.Move(moveVector);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            moveVector = Vector3.zero;
    }
}
