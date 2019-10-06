﻿using System.Collections.Generic;
using UnityEngine;

namespace Uitry
{
    public interface IModule
    {
        void Apply(Ship ship);
        void Update();
        void Damage(int delta_health);
        bool IsDead { get; }
        string Name { get; }
        int Health { get; }
        int EnergyConsumption { get; }
    }
}