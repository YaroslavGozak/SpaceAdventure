using System.Collections;
using System.Collections.Generic;
using Uitry;
using UnityEngine;

public class SolarPanelModule : ModuleBase, IModule
{
    private float _elapsed;
    private int _ramUsed = 2;
    public override string Name { get; set;}

    public override int EnergyConsumption => 0;

    public override int Ram => _ramUsed;

    public override void Apply(Ship ship)
    {
        ship.AddEnergy(3);
        base.Apply(ship);
    }

    public override void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= 1f)
        {
            _elapsed = _elapsed % 1f;
            AddEnergy();
        }
    }

    private void AddEnergy()
    {
        _ship.AddEnergy(5);
    }
}
