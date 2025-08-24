using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : GameSystem
{
    Dictionary<string, List<EnemyComponent>> enemies = new Dictionary<string, List<EnemyComponent>>();
    Dictionary<string, EnemyData> enemiesDict = new Dictionary<string, EnemyData>();

    Dictionary<string, List<BulletComponent>> bullets = new Dictionary<string, List<BulletComponent>>();
    Dictionary<string, WeaponData> weapons = new Dictionary<string, WeaponData>();

    public override void OnStart()
    {
        game.pool = this;
        foreach (var item in config.enemies.list)
        {
            enemies.Add(item.name, new List<EnemyComponent>());
            enemiesDict.Add(item.name, item);
        }
        foreach (var item in config.weapons.list)
        {
            bullets.Add(item.name, new List<BulletComponent>());
            weapons.Add(item.name, item);
        }
    }

    public EnemyComponent GetEnemy(string name, Vector3 pos)
    {
        EnemyComponent enemy;
        if (enemies[name].Count > 0)
        {
            enemy = enemies[name][0];
            enemies[name].RemoveAt(0);
            enemy.transform.position = pos;
            enemy.gameObject.SetActive(true);
            enemy.Ressurect();
            enemy.SetFrameStatus(true);
        }
        else
        {
            enemy = Instantiate(enemiesDict[name].prefab, pos, Quaternion.identity, transform);
            EnemyHealthBarFrame frame = Instantiate(config.enemies.healthBarFrame, screen.transform);
            frame.Prestart(game, enemy);
            enemy.SetFrame(frame);
        }

        enemy.Prestart(enemiesDict[name], game, config.enemies);
        game.enemies.Add(enemy.transform, enemy);
        return enemy;
    }
    public BulletComponent GetBullet(string name, Transform pivot)
    {
        BulletComponent bullet;
        if (bullets[name].Count > 0)
        {
            bullet = bullets[name][0];
            bullets[name].RemoveAt(0);
            bullet.transform.position = pivot.position;
            bullet.transform.rotation = pivot.rotation;
            bullet.gameObject.SetActive(true);
        }
        else
            bullet = Instantiate(weapons[name].bullet, pivot.position, pivot.rotation, transform);

        bullet.Prestart(game, weapons[name], config.weapons.bulletLideTime);
        game.bullets.Add(bullet);
        return bullet;
    }
    public void Return(EnemyComponent enemy)
    {
        enemy.SetFrameStatus(false);
        game.enemies.Remove(enemy.transform);
        enemy.gameObject.SetActive(false);
        enemies[enemy.enemyData.name].Add(enemy);
    }
    public void Return(BulletComponent bullet)
    {
        game.bullets.Remove(bullet);
        bullet.gameObject.SetActive(false);
        bullets[bullet.weapon.name].Add(bullet);
    }
}
