using Unity.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [field: SerializeField]  protected float health { get; private set; }
    protected float healthMax { get; private set; }
    protected bool isPlayer { get; private set; }
    public bool isDead { get; private set; }

    public void SetHealth(float healthMax, bool isPlayer)
    {
        this.isPlayer = isPlayer;
        this.healthMax = healthMax;
        health = healthMax;
    }

    public virtual void Damage(float damage)
    {
        health = Mathf.Max(health - damage, 0);

        if (health == 0)
            Death();
    }
    public virtual void Ressurect()
    {
        isDead = false;
        health = healthMax;
    }
    public virtual void Death()
    {
        isDead = true;
    }

    public virtual void Heal(float heal)
    {
        health = Mathf.Max(health + heal, healthMax);
    }

    public float GetCurrentHealth()
    {
        return health;
    }
}
