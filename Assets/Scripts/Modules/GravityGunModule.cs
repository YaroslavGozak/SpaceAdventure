using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uitry {   
    public class GravityGunModule : ModuleBase, IModule
    {
        private static int _energyConsumpsion = 1;
        public override string Name { get; set; }

        public override int EnergyConsumption => _energyConsumpsion;

        public override int RequiredRam => 3;

        // Update is called once per frame
        public override void Update()
        {
            if (_ship.IsDead) {
                Debug.Log("Module Died");
            }
            if (Input.GetKeyDown("space"))
            {
                _ship.SubstracEnergy(_energyConsumpsion);        
            }
        }

        public override void Apply(Ship Ship)
        {

        }
    }
}