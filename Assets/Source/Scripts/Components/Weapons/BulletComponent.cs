using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public float lifeTimer { get; private set; }
    protected GameData game;
    public WeaponData weapon { get; private set; }
    public virtual void Prestart(GameData game, WeaponData weapon, float LifeTime)
    {
        this.weapon = weapon;
        this.game = game;
        lifeTimer = LifeTime;
    }

    public virtual void Upd()
    {
        lifeTimer -= Time.deltaTime;
        transform.position += transform.forward * weapon.speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (game.enemies.ContainsKey(other.transform))
        {
            EnemyComponent enemy = game.enemies[other.transform];
            enemy.Damage(weapon.damage);
            if (enemy.GetCurrentHealth() <= 0)
                game.Kill?.Invoke();
            game.pool.Return(this);
        }
    }
}
