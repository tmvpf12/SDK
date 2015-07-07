using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElFunHouse.Champions.Fiora
{
    using ElFunHouse.Config;

    using LeagueSharp;
    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.Wrappers;

    internal class Fiora
    {
        #region Static Fields

        public static Dictionary<Standards.Spells, Spell> spells = new Dictionary<Standards.Spells, Spell>()
        {
            { Standards.Spells.Q, new Spell(SpellSlot.Q, 0) },
            { Standards.Spells.W, new Spell(SpellSlot.W, 0) },
            { Standards.Spells.E, new Spell(SpellSlot.E, 0) },
            { Standards.Spells.R, new Spell(SpellSlot.R, 0) }
        };

        #endregion

        public static void OnLoad()
        {
            CreateMenu.Load();
            Game.OnUpdate += OnUpdate;
            Console.WriteLine("Fiora retarded champion");
        }

        private static void OnUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Orbwalk:
                    break;

                case OrbwalkerMode.LastHit:
                    break;

                case OrbwalkerMode.LaneClear:
                    break;

                case OrbwalkerMode.Hybrid:
                    break;
            }
        }
    }
}
