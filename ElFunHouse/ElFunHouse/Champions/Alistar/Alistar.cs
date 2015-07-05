using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElFunHouse.Champions.Alistar
{
    using ElFunHouse.Config;

    using LeagueSharp;
    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.Extensions;
    using LeagueSharp.SDK.Core.UI.IMenu.Values;
    using LeagueSharp.SDK.Core.Utils;
    using LeagueSharp.SDK.Core.Wrappers;

    public class Alistar : Standards
    {
        #region Static Fields

        private static SpellSlot Flash;
        public static Dictionary<Spells, Spell> spells = new Dictionary<Spells, Spell>()
        {
            { Spells.Q, new Spell(SpellSlot.Q, 365) },
            { Spells.W, new Spell(SpellSlot.W, 650) },
            { Spells.E, new Spell(SpellSlot.E, 575) },
            { Spells.R, new Spell(SpellSlot.R, 0) }
        };

        #endregion

        public static bool CanCombo 
        {
            get
            {
                if (Player.Mana > Player.Spellbook.GetSpell(SpellSlot.Q).ManaCost + Player.Spellbook.GetSpell(SpellSlot.W).ManaCost)
                {
                    return true;
                }
                return false;
            }
        }

        public static void OnLoad()
        {
            CreateMenu.Load();
            Game.OnUpdate += OnUpdate;
            Flash = Player.GetSpellSlot("SummonerFlash");
            Console.WriteLine("Alistar");
        }

        private static void DoCombo()
        {
            var target = TargetSelector.GetTarget(spells[Spells.W].Range);
            if (target == null || !target.IsValidTarget())
            {
                return;
            }

            var useQ = menu["combo.settings"]["combo.spell.q"].GetValue<MenuBool>().Value;
            var useW = menu["combo.settings"]["combo.spell.w"].GetValue<MenuBool>().Value;

            Console.WriteLine(CanCombo);

            if (useQ && useW &&  spells[Spells.Q].IsReady() && spells[Spells.W].IsReady() && Player.Distance(target) < spells[Spells.W].Range  && CanCombo)
            {
                spells[Spells.W].Cast(target);
                var comboTime = Math.Max(0, Player.Distance(target) - 500) * 10 / 25 + 25;

                DelayAction.Add((int)comboTime, () => spells[Spells.Q].Cast());
            }

            if (useQ && useW && spells[Spells.Q].IsReady() && spells[Spells.W].IsReady()
                && Player.Distance(target) < spells[Spells.W].Range && Player.Distance(target) <= spells[Spells.Q].Range)
            {
                spells[Spells.Q].Cast();
            }
        }

        private static void DoHybrid()
        {
            var target = TargetSelector.GetTarget(spells[Spells.W].Range);
            if (target == null || !target.IsValidTarget())
            {
                return;
            }

            var useQ = menu["harass.settings"]["harass.spell.q"].GetValue<MenuBool>().Value;

            if (useQ && spells[Spells.Q].IsReady() && Player.Distance(target) <= spells[Spells.Q].Range)
            {
                spells[Spells.Q].Cast();
            }
        }

        private static void FlashHandler()
        {
            var useFlashCombo = menu["flash.settings"]["flash.combo"].GetValue<MenuKeyBind>().Active;
            if (!useFlashCombo)
            {
                return;
            }

            Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);

            var target = TargetSelector.GetTarget(spells[Spells.W].Range);
            if (target == null || !target.IsValidTarget() || !spells[Spells.Q].IsReady() || !spells[Spells.W].IsReady())
            {
                return;
            }

            Orbwalker.Orbwalk(target: target, position: Game.CursorPos);
            Player.Spellbook.CastSpell(Flash, target.Position);
            spells[Spells.Q].Cast();

            DelayAction.Add((int) 1000, () => spells[Spells.W].Cast(target));
        }

        private static void OnUpdate(EventArgs args)
        {

            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Orbwalk:
                    DoCombo();
                    break;

                case OrbwalkerMode.LastHit:
                    break;

                case OrbwalkerMode.LaneClear:
                 
                    break;

                case OrbwalkerMode.Hybrid:
                    DoHybrid();
                    break;
            }

            FlashHandler();
        }
    }
}
