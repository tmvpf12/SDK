namespace ElRengar
{
    using System;
    using System.Collections.Generic;

    using ElRengar.Config;

    using LeagueSharp;
    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.Extensions;
    using LeagueSharp.SDK.Core.UI.IMenu.Values;
    using LeagueSharp.SDK.Core.Wrappers;

    internal class Rengar : Standards
    {
        #region Static Fields

        public static Dictionary<Spells, Spell> spells = new Dictionary<Spells, Spell>()
        {
            { Spells.Q, new Spell(SpellSlot.Q, 300) },
            { Spells.W, new Spell(SpellSlot.W, 500) },
            { Spells.E, new Spell(SpellSlot.E, 1000) },
            { Spells.R, new Spell(SpellSlot.R, 2000) }
        };

        #endregion

        #region Public Methods and Operators

        public static void OnLoad(object sender, EventArgs e)
        {
            if (Player.CharData.BaseSkinName != "Rengar")
            {
                Console.WriteLine("Champion is not supported.");
                return;
            }

            try
            {
                CreateMenu();
                CalculateRange();
                Visibility();
                Game.OnUpdate += OnUpdate;
                spells[Spells.E].SetSkillshot(0.25f, 70f, 1500f, true, SkillshotType.SkillshotLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region Methods

        // UNSEEN PREDATOR: While in brush or in stealth, Rengar gains bonus attack range 
        // and his basic attacks cause him to leap at the target's location.
        private static void CalculateRange()
        {
            if (Player.AttackRange > 150 && Player.AttackRange < 700)
            {
                LeapRange = (int)(Player.AttackRange + 175);
            }

            if (Player.AttackRange > 150 && Player.AttackRange >= 700)
            {
                LeapRange = (int)(Player.AttackRange + 75);
            }

            if (Player.AttackRange < 150)
            {
                LeapRange = 125;
            }

            spells[Spells.Q].Range = LeapRange + 25;
        }

        private static void DoCombo()
        {
            var target = TargetSelector.GetTarget();
            if (target == null || !target.IsValidTarget())
            {
                target = TargetSelector.GetTarget(spells[Spells.E].Range);
            }

            if (!target.IsValidTarget(spells[Spells.E].Range))
            {
                return;
            }

            var useQ = menu["combo.settings"]["combo.spell.q"].GetValue<MenuBool>().Value;
            var useW = menu["combo.settings"]["combo.spell.w"].GetValue<MenuBool>().Value;
            var useE = menu["combo.settings"]["combo.spell.e"].GetValue<MenuBool>().Value;

            if (Ferocity <= 4)
            {
                if (spells[Spells.Q].IsReady())
                {
                    spells[Spells.Q].Cast();
                }

                if (IsVisible && spells[Spells.E].IsReady() && Player.Distance(target) <= spells[Spells.E].Range)
                {
                    var pred = spells[Spells.E].GetPrediction(target);
                    if (pred.Hitchance >= HitChance.High)
                    {
                        spells[Spells.E].Cast(target);
                    }
                }

                if (spells[Spells.W].IsReady() && target.IsValidTarget(spells[Spells.W].Range))
                {
                    spells[Spells.W].Cast();
                }
            }

            if (Ferocity == 5)
            {
                if (spells[Spells.Q].IsReady())
                {
                    spells[Spells.Q].Cast();
                }

                if (spells[Spells.E].IsReady())
                {
                    var pred = spells[Spells.E].GetPrediction(target, false);
                    if (pred.Hitchance >= HitChance.High)
                    {
                        spells[Spells.E].Cast(target);
                    }
                }

                if (spells[Spells.W].IsReady() && target.IsValidTarget(spells[Spells.W].Range))
                {
                    spells[Spells.W].Cast();
                }
            }
        }

        private static void DoHybrid()
        {
        }

        private static void DoLaneClear()
        {
        }

        private static void DoLastHit()
        {
        }

        private static void OnUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Orbwalk:
                    DoCombo();
                    //Console.WriteLine(Ferocity);
                    break;

                case OrbwalkerMode.LastHit:
                    DoLastHit();
                    break;

                case OrbwalkerMode.LaneClear:
                    DoLaneClear();
                    break;

                case OrbwalkerMode.Hybrid:
                    DoHybrid();
                    break;
            }
        }

        private static void Visibility()
        {
            if (LeapRange > 150)
            {
                IsVisible = false;
            }
            else
            {
                IsVisible = true;
            }
        }

        #endregion
    }
}