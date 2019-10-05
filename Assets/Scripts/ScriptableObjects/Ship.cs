using System.Collections.Generic;
using Uitry;
using UnityEngine;

namespace Uitry
{
    public class Ship
    {
        private static Ship _instance = new Ship();

        private Ship() {
            Modules = new List<IModule>();
            Energy = 10000;
            RAM = 8;
        }
        public static Ship Instance { get
            {
                return _instance;
            }
        }
        public int Energy { get; private set; }
        public int RAM { get; private set; }

        public List<IModule> Modules { get; set; }
        public bool IsDead { get; internal set; }

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