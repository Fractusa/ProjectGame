using UnityEngine;

public class BleedEffect : StatusEffect
{
    private float duration;
    private float tickInterval;
    private float timer;
    private float elapsed;

    private int damagePerTick;
    public BleedEffect(EnemyHealth target, int damage, float tickInterval, float duration) : base("Bleeding", target)
    {
        this.damagePerTick = damage;
        this.tickInterval = tickInterval;
        this.duration = duration;
    }

    public override void Apply()
    {
        Debug.Log("Enemy is bleeding!");
        elapsed = 0f;
        timer = 0f;
        IsFinished = false;
    }

    public override void Update(float deltaTime)
    {
        elapsed += deltaTime;
        timer += deltaTime;

        if (timer >= tickInterval)
        {
            target.TakeDamage(DamageInfo.FromEffect(damagePerTick, DamageType.Bleed));
            timer = 0f;
        }

        if (elapsed >= duration)
        {
            IsFinished = true;
        }
    }

    public override void Refresh()
    {
        Debug.Log("Bleeding effect refreshed!");
        elapsed = 0f;
    }
    public override void Remove()
    {
        Debug.Log("Bleeding effect ended");
    }
}
