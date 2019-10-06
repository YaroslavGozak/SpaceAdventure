using System;
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

        public void UpdateModules()
        {
            foreach (var module in Modules)
            {
                module.Update();
                if (module.IsDead)
                {
                    RemoveModule(module);
                }
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
            OnModulaRemove?.Invoke(this, new ModuleRemoveEventArgs { Module = module });
        }

        public void SubstracEnergy(int delta)
        {
            if(this.Energy > 0)
            {
                this.Energy -= delta;
                if (this.Energy <= 0)
                {
                    Debug.Log("You Died");
                }
            }
        }

        public void AddEnergy(int delta)
        {
            this.Energy += delta;
        }

        public void AddRam(int ram)
        {
            this.RAM += ram;
        }

        public void SubstractRam(int ram)
        {
            this.RAM -= ram;
        }

        public event ModuleRemoveEventHandler OnModulaRemove;
    }
}