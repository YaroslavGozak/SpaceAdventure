using System;
using System.Collections;
using System.Collections.Generic;
using Uitry;
using UnityEngine;

namespace Uitry
{
    public class ModuleRemoveEventArgs : EventArgs
    {
        public IModule Module { get; set; }
    }

    public delegate void ModuleRemoveEventHandler(object sender, ModuleRemoveEventArgs args);
}

