using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uitry {   
    public class GravityGunModule : IModule
    {
        public static int energy_consumpsion = 1;
        public static int health = 3;
        public string Name { get; private set; } = "Gravity Module";

        public bool IsDead => throw new System.NotImplementedException();

        // Update is called once per frame
        public void Update(Ship Ship)
        {
            if (Ship.IsDead) {
                Debug.Log("Module Died");
            }
            if (Input.GetKeyDown("space"))
            {
                Ship.SubstracEnergy(energy_consumpsion);        
            }
        }

        public void Apply(Ship Ship)
        {

        }

        public void Damage(int delta_health)
        {
            health -= delta_health;
            if (health <= 0) {
                Debug.Log("Module Died");
            }
        }
    }
}