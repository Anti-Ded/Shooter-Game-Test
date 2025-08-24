using UnityEngine;

public class EnemyComponent : HealthComponent
{
    [SerializeField] CharacterController controller;
    public EnemyData enemyData { get; private set; }

    float fallSpeed;

    protected GameData game;
    protected EnemiesConfig enemiesConfig;

    EnemyHealthBarFrame frame;

    public void SetFrame(EnemyHealthBarFrame frame)
    {
        this.frame = frame;
    }
    public virtual void Prestart(EnemyData enemy, GameData game, EnemiesConfig enemiesConfig)
    {
        this.enemiesConfig = enemiesConfig;
        this.game = game;
        this.enemyData = enemy;
        
        SetHealth(enemy.health, false);
    }
    public void LateUpd()
    {
        frame.LateUpd();
    }
    public virtual void Upd()
    {
        if (isDead) return;

        // Look at player;
        Vector3 point = game.player.transform.position;
        point.y = transform.position.y;
        transform.LookAt(point);

        // Fall
        if (!controller.isGrounded)
            fallSpeed += Time.deltaTime;
        else fallSpeed = 0;

        controller.Move((transform.forward * enemyData.speed - Vector3.up * fallSpeed) * Time.deltaTime);
    }

    public void Drop(Vector3 direction)
    {
        controller.Move(direction * Time.deltaTime);
    }

    public override void Death()
    {
        base.Death();
        game.pool.Return(this);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == game.player.transform && !isDead)
        {
            game.player.Damage(enemyData.playerHitDamage);
            game.pool.Return(this);
            base.Death();
        }
    }

    public void SetFrameStatus(bool status)
    {
        frame.gameObject.SetActive(status);
    }
}
