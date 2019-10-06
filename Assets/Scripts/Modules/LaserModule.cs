using System.Collections;
using System.Collections.Generic;
using Uitry;
using UnityEngine;

public class LaserModule : ModuleBase, IModule
{
    public override string Name { get; set; }

    public override int EnergyConsumption => 2;

    public override int RequiredRam => 5;

    public override void Update()
    {

    }
}
