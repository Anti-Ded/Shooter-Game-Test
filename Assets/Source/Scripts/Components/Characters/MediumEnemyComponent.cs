using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyComponent : EnemyComponent
{
    List<DroppedEnemy> droppedEnemies = new List<DroppedEnemy>();
    public override void Upd()
    {
        base.Upd();

        for (int i = droppedEnemies.Count - 1; i >= 0; i--)
            if (!droppedEnemies[i].enemy.isDead && droppedEnemies[i].timer > 0)
            {
                droppedEnemies[i].timer -= Time.deltaTime;
                droppedEnemies[i].enemy.Drop(droppedEnemies[i].direction);
                droppedEnemies[i].direction = Vector3.Lerp(droppedEnemies[i].direction, Vector3.zero, Time.deltaTime);
            }
            else droppedEnemies.RemoveAt(i);
    }
    public override void Ressurect()
    {
        base.Ressurect();
        droppedEnemies.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (game.enemies.ContainsKey(other.transform) && game.enemies[other.transform].enemyData.name == enemiesConfig.nameOfEnemyThatMediumDrops)
            droppedEnemies.Add(new DroppedEnemy(game.enemies[other.transform], transform.forward * enemiesConfig.dropPower + Vector3.up * 2f, enemiesConfig.dropTime));
        else if (other.transform == game.player.transform && !isDead)
        {
            game.player.Damage(enemyData.playerHitDamage);
            base.Death();
        }
    }

    class DroppedEnemy
    {
        public EnemyComponent enemy;
        public Vector3 direction;
        public float timer;
        public DroppedEnemy(EnemyComponent enemy, Vector3 direction, float timer)
        {
            this.timer = timer;
            this.enemy = enemy;
            this.direction = direction;
        }
    }
}
