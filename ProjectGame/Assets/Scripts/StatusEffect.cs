using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    public string Name { get; private set; }
    protected EnemyHealth target;
    public bool IsFinished { get; protected set; }

    public virtual int MaxStacks => 1; //default set to non-stackable
    public static Dictionary<System.Type, int> ActiveStacks = new Dictionary<System.Type, int>();

    protected StatusEffect(string name, EnemyHealth target)
    {
        Name = name;
        this.target = target;
        IsFinished = false;
    }

    public abstract void Apply(); //Called when effect starts
    public abstract void Update(float deltaTime); //Called every frame/tick
    public abstract void Refresh(); //Called if re-applied
    public abstract void Remove(); //Called when effect ends


    public virtual void OnMaxStacksReached()
    {
        //Default does nothing, override to change
    }
}
