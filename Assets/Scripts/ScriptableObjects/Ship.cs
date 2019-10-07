﻿using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Uitry;
using UnityEngine;

namespace Uitry
{
    public class Ship
    {
        private static Ship _instance = new Ship();
        private int _defaultRam = 10;
        private int _additionalRam = 0;

        public bool IsHubAttached { get; private set; }
        public bool IsToraxAlive { get; private set; }
        private Ship() {
            Modules = new List<IModule>();
            Energy = 10000;
            Score = 0;
            IsHubAttached = false;
            IsToraxAlive = false;
            Mesage = ">_";
        }
        public static Ship Instance { get
            {
                return _instance;
            }
        }
        public int Energy { get; private set; }
        public int Score { get; private set; }
        public string Mesage { get; private set; }
        public int RAM => _defaultRam + _additionalRam - Modules.Sum(module => module.RequiredRam);

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
            //AddMsg("Ram: " + RAM);
            if (RAM < module.RequiredRam)
                throw new RamNotEnoughException("Ram not enough for module");
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
            _additionalRam += ram;
        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void NullScore()
        {
            Score = 0;
        }

        public void SubstractRam(int ram)
        {
            _additionalRam -= ram;
            if (_additionalRam < 0)
                _additionalRam = 0;
        }

        public void AddMsg(string msg)
        {
            Mesage = ">_" + msg;
        }

        public void AddHub()
        {
            IsHubAttached = true;
            Energy += 200;
        }

        public void RemoveHub()
        {
            IsHubAttached = false;
        }

        public void AddLaser()
        {
            IsHubAttached = true;
            Energy += 200;
        }

        public void RemoveLaser()
        {
            IsHubAttached = true;
            Energy += 200;
        }
        public void AddTorax()
        {
            IsToraxAlive = true;
        }
        public void RemoveTorax()
        {
            IsToraxAlive = false;
        }

        public event ModuleRemoveEventHandler OnModulaRemove;
    }
}