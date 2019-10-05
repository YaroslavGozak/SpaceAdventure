using System.Collections.Generic;
using UnityEngine;

namespace Uitry
{
    [CreateAssetMenu(menuName = "Ship Modules")]
    public class ShipModules : ScriptableObject
    {
        public string Description;
        public int RAM_needed;
        public int Free_docking_space_needed;

        public List<SpaceShipAttributes> AffectedAttributes = new List<SpaceShipAttributes>();
    }
}
