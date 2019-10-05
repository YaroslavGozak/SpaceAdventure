using System.Collections.Generic;
using UnityEngine;

namespace Uitry
{
    public interface IModule
    {
        void Apply(Ship Ship);
        void Update(Ship Ship);

        void Damage(int delta_health);
        bool IsDead { get; }
        string Name { get; }

    }
}