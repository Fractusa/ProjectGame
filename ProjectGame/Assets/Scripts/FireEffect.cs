using UnityEngine;

public class FireEffect : StatusEffect
{
    private float duration;
    private float tickInterval;
    private float timer;
    private float elapsed;

    private int damagePerTick;
    public FireEffect(EnemyHealth target, int damage, float tickInterval, float duration) : base("Burning", target)
    {
        this.damagePerTick = damage;
        this.tickInterval = tickInterval;
        this.duration = duration;
    }

    public override int MaxStacks => 5; //Sets max fire stacks

    public override void Apply()
    {
        Debug.Log("Enemy is burning!");
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
            target.TakeDamage(damagePerTick);
            timer = 0f;
        }

        if (elapsed >= duration)
        {
            IsFinished = true;
        }
    }

    public override void Refresh()
    {
        Debug.Log("Fire refreshed!");
        elapsed = 0f; //Reset the timer
    }
    public override void Remove()
    {
        Debug.Log("Burning effect ended");
    }

    public override void OnMaxStacksReached()
    {
        Debug.Log("Max fire stacks reached");
        target.TakeDamage(50); //Trigger max stack event
    }
}
