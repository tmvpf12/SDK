using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElFunHouse
{
    class BaseHandler
    {
        internal static void Load(string champName)
        {
            switch (champName)
            {
                case "Alistar":
                    Champions.Alistar.Alistar.OnLoad();
                    break;
            }
        }
    }
}
