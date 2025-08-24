using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField] protected Transform[] pivots;

    protected GameData game;
    protected WeaponData weaponData;
    protected WeaponsConfig weaponsConfig;

    [field: SerializeField] protected float reloadTimer;
    [field: SerializeField] protected float fireTimer;
    [field: SerializeField] protected float ammo;

    [field: SerializeField] protected int pivotIndex;
    [field: SerializeField] protected bool orderToFire;
    public virtual void Prestart(WeaponData weaponData, GameData game, WeaponsConfig weaponsConfig)
    {
        this.weaponsConfig = weaponsConfig;
        this.weaponData = weaponData;
        this.game = game;
        ammo = weaponData.ammoMax;
    }
    public string GetWeaponName()
    {
        return weaponData.name;
    }
    public virtual void Fire()
    {
        game.pool.GetBullet(weaponData.name, pivots[pivotIndex]);
    }
    public virtual void Upd()
    {
        fireTimer -= Time.deltaTime;
        
        reloadTimer = reloadTimer > 0 ? reloadTimer - Time.deltaTime : 0;
        if (reloadTimer < 0)
            ammo = weaponData.ammoMax;

        if (orderToFire && ammo > 0 && fireTimer <= 0)
        {
            Fire();
            ammo--;
            if (ammo == 0)
                reloadTimer = weaponData.reloadTime;
            pivotIndex = (pivotIndex + 1) % pivots.Length;
            fireTimer = 1f / weaponData.fireRatePerSec;
        }
    }

    public void OrderToFire(bool status)
    {
        orderToFire = status;
    }

    public string GetCurrentStatus()
    {
        if (ammo > 0)
            return "AMMO " + ammo + "/" + weaponData.ammoMax;
        else return "RELOAD " + (100f * (weaponData.reloadTime - reloadTimer) / weaponData.reloadTime).ToString("F0") + "%";
    }
}

