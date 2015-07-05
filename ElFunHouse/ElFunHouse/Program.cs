using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.SDK.Core.Events;
using ElFunHouse.Config;

namespace ElFunHouse
{
    class Program : Standards
    {
        static void Main(string[] args)
        {
            Load.OnLoad += (sender, eventArgs) =>
            {
                try
                {
                    BaseHandler.Load(Player.CharData.BaseSkinName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            };
        }
    }
}
