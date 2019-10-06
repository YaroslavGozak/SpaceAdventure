using System.Collections;
using System.Collections.Generic;
using Uitry;
using UnityEngine;

public abstract class ModuleBase : IModule
{
    protected int _health = 100;

    protected Ship _ship;
    public virtual bool IsDead => Health <= 0;

    public abstract string Name { get; set; }

    public virtual int Health => _health;

    public abstract int EnergyConsumption { get; }

    public abstract int Ram { get; }

public virtual void Apply(Ship ship)
    {
        _ship = ship;
    }

    public abstract void Update();

    public virtual void Damage(int delta_health)
    {
        _health -= delta_health;

        if (IsDead)
        {
            Debug.Log($"Module {this.GetType().Name} is dead. Detaching from ship");
            _ship.RemoveModule(this);
        }
           
    }
}
