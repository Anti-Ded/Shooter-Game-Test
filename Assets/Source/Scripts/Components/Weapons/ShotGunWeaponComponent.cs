using System.Collections.Generic;
using UnityEngine;

public class ShotGunWeaponComponent : WeaponComponent
{
    List<Vector3> pivotsStartPoses = new List<Vector3>();
    public override void Prestart(WeaponData weaponData, GameData game, WeaponsConfig weaponsConfig)
    {
        base.Prestart(weaponData, game, weaponsConfig);
        for (int i = 0; i < pivots.Length; i++)
            pivotsStartPoses.Add(pivots[i].localEulerAngles);
    }
    public override void Fire()
    {
        for (int i = 0; i < weaponsConfig.shotgunBulletsCount; i++)
        {
            pivots[pivotIndex].transform.localEulerAngles = pivotsStartPoses[pivotIndex] + RandomVector3();
            game.pool.GetBullet(weaponData.name, pivots[pivotIndex]);
        }
    }

    Vector3 RandomVector3()
    {
        return new Vector3(0, Random.Range(-1f, 1f), 0) * weaponsConfig.shotgunDispersionAngle;
    }
}
