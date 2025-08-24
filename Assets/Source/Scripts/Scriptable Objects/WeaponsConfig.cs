using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/WeaponsConfig")]
public sealed class WeaponsConfig : ScriptableObject
{
    public float bulletLideTime = 10;
    public int shotgunBulletsCount;
    public float shotgunDispersionAngle = 2;
    public List<WeaponData> list = new List<WeaponData>();
}

[Serializable] public class WeaponData
{
    public string name;
    public float damage;
    public float ammoMax;
    public float fireRatePerSec;
    public float reloadTime;
    public float speed;
    public WeaponComponent weapon;
    public BulletComponent bullet;
}

