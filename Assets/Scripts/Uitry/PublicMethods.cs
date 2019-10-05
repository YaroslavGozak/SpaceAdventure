using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uitry
{
    [System.Serializable]
    public class SpaceShipAttributes
    {
        public ShipAttributes attribute;
        public int amount;

        public SpaceShipAttributes(ShipAttributes attribute, int amount)
        {
            this.attribute = attribute;
            this.amount = amount;
        }
    }
}
