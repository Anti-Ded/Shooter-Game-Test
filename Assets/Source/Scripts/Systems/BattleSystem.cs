using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : GameSystem
{
    List<WeaponComponent> weapons = new List<WeaponComponent>();
    int weaponIndex;
    float scrollValue;
    public override void OnStart()
    {
        for (int i = 0; i < config.weapons.list.Count; i++)
        {
            WeaponData data = config.weapons.list[i];
            WeaponComponent weapon = Instantiate(data.weapon, game.player.transform.position, game.player.transform.rotation, game.player.transform);
            weapon.Prestart(data, game, config.weapons);
            weapons.Add(weapon);
            weapon.gameObject.SetActive(i == 0);
        }
        game.currentWeapon = weapons[0];
        game.Kill += Kill;

        screen.restartButton.gameObject.SetActive(false);
        screen.restartButton.onClick.AddListener(Restart);
    }
    void Restart()
    {
        SceneManager.LoadScene(0);
    }
    void Kill()
    {
        save.kills++;
    }
    public override void OnLateUpdate()
    {
        foreach (var item in game.enemies)
            item.Value.LateUpd();
    }
    public override void OnUpdate()
    {
        UI();
        PlayerDied();

        if (game.player.isDead) return;

        WeaponSwitcher();
        CurrentWeaponFire();

        for (int i = game.bullets.Count - 1; i >= 0; i--)
            if (game.bullets[i].lifeTimer > 0)
                game.bullets[i].Upd();
            else game.pool.Return(game.bullets[i]);
    }

    void PlayerDied()
    {
        if (game.player.isDead && !game.gameover)
        {
            game.gameover = true;
            save.deaths++;
            game.saveLoad.Save();
            screen.restartButton.gameObject.SetActive(true);
        }
    }

    void UI()
    {
        screen.deathsText.text = "DEATHS " + save.deaths;
        screen.killsText.text = "KILLS " + save.kills;
        screen.healthText.text = "HEALTH " + game.player.GetCurrentHealth();
        screen.ammoText.text = game.currentWeapon.GetCurrentStatus();

        
    }
    private void WeaponSwitcher()
    {
        scrollValue = Mathf.Clamp(scrollValue + Input.mouseScrollDelta.y, 0, weapons.Count - 1);
        int newIndex = Mathf.RoundToInt(scrollValue);
        if (newIndex != weaponIndex)
            SwitchWeapon(newIndex);
    }

    private void CurrentWeaponFire()
    {
        if (Input.GetMouseButtonDown(0))
            game.currentWeapon.OrderToFire(true);
        else if (Input.GetMouseButtonUp(0))
            game.currentWeapon.OrderToFire(false);
        game.currentWeapon.Upd();
    }

    void SwitchWeapon(int index)
    {
        weaponIndex = index;
        game.currentWeapon.gameObject.SetActive(false);
        game.currentWeapon.OrderToFire(false);
        game.currentWeapon = weapons[index];
        game.currentWeapon.gameObject.SetActive(true);
    }
}
