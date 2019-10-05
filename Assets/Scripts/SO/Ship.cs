using System.Collections.Generic;
using Uitry;
using UnityEngine;

namespace Uitry
{
    public class Ship
    {

        public int Energy { get; private set; }
        public int RAM { get; private set; }

        public List<IModule> Modules { get; set; }
        public bool IsDead { get; internal set; }

        public Ship() { Modules = new List<IModule>(); }

        public void UpdateModule()
        {
            foreach (var module in Modules)
            {
                module.Update(this);
            }
        }
        public void AddModule(IModule module)
        {
            Modules.Add(module);
            module.Apply(this);
        }

        public void RemoveModule(IModule module)
        {
            Modules.Remove(module);
        }

        public void SubstracEnergy(int delta)
        {
            this.Energy -= delta;
            if (this.Energy <= 0)
            {
                Debug.Log("You Died");
            }
        }

    }
}