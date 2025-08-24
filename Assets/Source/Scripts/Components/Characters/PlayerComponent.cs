using UnityEngine;

public class PlayerComponent : HealthComponent
{
    [SerializeField] CharacterController controller;

    GameData game;
    PlayerConfig playerConfig;

    float fallSpeed;
    Vector3 moveDirection;

    public void Prestart(GameData game, PlayerConfig playerConfig)
    {
        this.playerConfig = playerConfig;
        this.game = game;
        SetHealth(playerConfig.playerMaxHealth, true);
    }

    public void Move(Vector3 direction)
    {
        if (isDead) return;

        direction = Quaternion.Euler(game.cameraController.transform.eulerAngles) * direction;
        moveDirection = direction.normalized * playerConfig.playerSpeed;

        if (!controller.isGrounded)
            fallSpeed += Time.deltaTime;
        else fallSpeed = 0;

        controller.Move((moveDirection - Vector3.up * fallSpeed) * Time.deltaTime);
    }

    public void Targeting(Vector3 point)
    {
        transform.LookAt(point);
    }
}
