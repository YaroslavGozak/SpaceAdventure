using System.Collections.Generic;
using UnityEngine;

namespace Uitry
{

    public class CoreModuleBehaviour : MonoBehaviour
    {
        [Header("Core Module")]
        public int RAM_slots_used = 1;
        public int Energy_level = 10;
        
        [Header("Ship Attribute")]
        public List<SpaceShipAttributes> Attributes = new List<SpaceShipAttributes>();

        [Header("Ship Modules Enabled")]
        public List<ShipModules> SpaceShipModules = new List<ShipModules>();
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}