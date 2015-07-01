﻿namespace ElRengar
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Linq;

    using ElRengar.Config;

    using LeagueSharp;
    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.Extensions;
    using LeagueSharp.SDK.Core.UI.IMenu.Values;
    using LeagueSharp.SDK.Core.Wrappers;
    using LeagueSharp.SDK.Core.Events;
    using LeagueSharp.SDK.Core.Utils;

    using SharpDX;

    internal class Rengar : Standards
    {
        #region Static Fields

        public static Dictionary<Spells, Spell> spells = new Dictionary<Spells, Spell>()
        {
            { Spells.Q, new Spell(SpellSlot.Q, Player.AttackRange) },
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
                Game.OnUpdate += OnUpdate;
                Orbwalker.OnAction += OnAction;
                Dash.OnDash += OnDash;
                Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;

                spells[Spells.E].SetSkillshot(0.25f, 70f, 1500f, true, SkillshotType.SkillshotLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region Methods

        private static void DoCombo()
        {
            var target = TargetSelector.GetTarget(spells[Spells.E].Range);
            if (target == null || !target.IsValidTarget())
            {
                return;
            }

            var useQ = menu["combo.settings"]["combo.spell.q"].GetValue<MenuBool>().Value;
            var useW = menu["combo.settings"]["combo.spell.w"].GetValue<MenuBool>().Value;
            var useE = menu["combo.settings"]["combo.spell.e"].GetValue<MenuBool>().Value;
            var useEoutOfRange = menu["combo.settings"]["combo.spell.e.outofrange"].GetValue<MenuBool>().Value;
            var prioritized = menu["combo.settings"]["combo.prioritize"].GetValue<MenuList>();

            if (Felicity <= 4)
            {
                if (useQ && spells[Spells.Q].IsReady() && Player.Distance(target) < spells[Spells.Q].Range + 0x1e)
                {
                    spells[Spells.Q].Cast();
                }

                if (RengarR) return;

                if (useE && spells[Spells.E].IsReady() && Player.Distance(target) < spells[Spells.E].Range)
                {
                    //waiting for prediction
                    spells[Spells.E].Cast(target);
                }

                if (useW && spells[Spells.W].IsReady() && Player.Distance(target) < spells[Spells.W].Range)
                {
                    spells[Spells.W].Cast();
                }
            }

            if (Felicity == 5)
            {
                switch (prioritized.Index)
                {
                    case 0:
                        if (useQ && spells[Spells.Q].IsReady() && Player.Distance(target) < spells[Spells.Q].Range + 0x32)
                        {
                            spells[Spells.Q].Cast();
                        }
                        break;

                    case 1:
                        if (useW && spells[Spells.W].IsReady() && Player.Distance(target) < spells[Spells.W].Range && !RengarR)
                        {
                            spells[Spells.W].Cast();
                        }
                      
                        break;

                    case 2:
                        if (useE && spells[Spells.E].IsReady() && !RengarR)
                        {
                            //waiting for prediction
                            spells[Spells.E].Cast(target);
                        }
                        break;
                }

                if (useEoutOfRange && Player.Distance(target) > spells[Spells.Q].Range + 0x64)
                {
                    spells[Spells.E].Cast(target);
                }
            }

            ItemHandler();
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

            NotificationHandler();
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                if (args.SData.Name == "RengarR" && Items.CanUseItem(3142))
                {
                    DelayAction.Add(1000, () => Items.UseItem(3142));
                }
            } 
        }

        #region OnAction
        private static void OnAction(object sender, Orbwalker.OrbwalkerActionArgs orbwalk)
        {
            var target = orbwalk.Target; // I hope this is fine.
            if (!target.IsValidTarget())
                return;

            if (orbwalk.Type == OrbwalkerType.AfterAttack && spells[Spells.Q].IsReady() && target.IsValidTarget() && Felicity == 0x5 && ActiveMode == OrbwalkerMode.Hybrid || ActiveMode == OrbwalkerMode.Orbwalk)
            {
                spells[Spells.Q].Cast();
            }
        }
        #endregion

        private static void OnDash(object sender, Dash.DashArgs e)
        {
            if (e.Unit.IsMe)
            {
                if (Orbwalker.ActiveMode == OrbwalkerMode.Orbwalk || Orbwalker.ActiveMode == OrbwalkerMode.Hybrid)
                {
                    var prioritized = menu["combo.settings"]["combo.prioritize"].GetValue<MenuList>();
                    var target = TargetSelector.GetTarget(spells[Spells.E].Range);
                    if (target == null || !target.IsValidTarget())
                    {
                        return;
                    }

                    switch (prioritized.Index)
                    {
                        case 0:
                            if (spells[Spells.Q].IsReady() && Player.Distance(target) < spells[Spells.Q].Range + 0x32)
                            {
                                spells[Spells.Q].Cast();
                            }
                            break;

                        case 2:
                            if (spells[Spells.E].IsReady() && Vector3.Distance(Player.ServerPosition, target.ServerPosition) <= LeapRange)
                            {
                                spells[Spells.E].Cast(target);
                            }
                            break;
                    }

                    ItemHandler(); 
                }
            }
            else
            {
                return;
            }
        }

        #endregion
    }
}