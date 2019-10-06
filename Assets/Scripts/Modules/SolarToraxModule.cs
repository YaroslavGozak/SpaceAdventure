using System.Collections;
using System.Collections.Generic;
using Uitry;
using UnityEngine;

public class SolarToraxModule : ModuleBase, IModule
{
    public override string Name { get; set; }

    public override int EnergyConsumption => 0;

    public override int RequiredRam => 1;

    public override void Update()
    {

    }

    public override void Apply(Ship ship)
    {
        ship.AddEnergy(200);
        base.Apply(ship);
    }
}
