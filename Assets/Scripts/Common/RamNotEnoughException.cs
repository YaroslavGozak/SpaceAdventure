using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Common
{
    class RamNotEnoughException: Exception
    {
        public RamNotEnoughException(string message):base(message)
        {}
    }
}
