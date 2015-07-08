// This file is part of LeagueSharp.Common.
// 
// LeagueSharp.Common is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// LeagueSharp.Common is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with LeagueSharp.Common.  If not, see <http://www.gnu.org/licenses/>.
namespace ElRengar.Config
{
    #region

    using System;
    using System.Windows.Forms;

    using LeagueSharp;
    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.Events;
    using LeagueSharp.SDK.Core.Extensions;
    using LeagueSharp.SDK.Core.UI.IMenu.Values;
    using LeagueSharp.SDK.Core.UI.INotifications;
    using LeagueSharp.SDK.Core.Wrappers;

    using SharpDX;

    using Menu = LeagueSharp.SDK.Core.UI.IMenu.Menu;

    #endregion

    public enum Spells
    {
        Q,

        W,

        E,

        R
    }

    internal class Standards
    {
        #region Static Fields

        public static Menu menu;

        public static OrbwalkerMode ActiveMode { get; set; }

        private static Notification notification;

        public static SpellSlot Ignite;

        public static SpellSlot Smite;

        public static Items.Item Botrk, Cutlass;

        public static String ScriptVersion { get { return typeof(Rengar).Assembly.GetName().Version.ToString(); } }

        #endregion

        #region Public Properties

        public static int Felicity //Felicity Smoak makes me Rengarrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr
        {
            get
            {
                return (int)ObjectManager.Player.Mana;
            }
        }

        public static Obj_AI_Hero Player
        {
            get
            {
                return ObjectManager.Player;
            }
        }
        public static bool HasPassive
        {
            get
            {
                return Player.HasBuff("rengarpassivebuff");
            }
        }

        public static bool RengarQbuffMax //riot is retarded lmao kappahd 
        {
            get
            {
                return Player.HasBuff("RengarQbuffMAX");
            }
        }

        //
        public static bool Rengartrophyicon6 //riot is retarded lmao kappahd 
        {
            get
            {
                return Player.HasBuff("rengarbushspeedbuff");
            }
        }

        public static int LeapRange
        {
            get
            {
                if (HasPassive && Rengartrophyicon6)
                {
                    return 725;
                }

                return 600;
            }
        }

        public static bool RengarR
        {
            get
            {
                return Player.HasBuff("rengarr");
            }
        }

        #endregion

        #region Methods

        protected static void ItemHandler(Obj_AI_Base target)
        {
            if (Player.IsDashing() || !target.IsValidTarget())
            {
                return;
            }

            /*if (Items.CanUseItem(3074)) Items.UseItem(3074); //Hydra
            if (Items.CanUseItem(3077)) Items.UseItem(3077); //Tiamat*/

            //Cutlass = new Items.Item(3144, 450f);
           // Botrk = new Items.Item(3153, 450f);

            if (Items.CanUseItem(3144)) Items.UseItem(3144); //Cutlass
            if (Items.CanUseItem(3153)) Items.UseItem(3153); //Botrk
            if (Items.CanUseItem(3142)) Items.UseItem(3142); //Ghostblade 

            /*new Items.Item(3144, 450f).Cast(target);
            new Items.Item(3153, 450f).Cast(target);*/

            Console.WriteLine("Casted");
        }

        protected static void NotificationHandler()
        {
            //Waiting for old Notifications
        }

        /*
        * PLEASE THIS MENU IS PATENTED BY AUSTRALIAN IP LAWS.
        * KEEP CALM AND (); {} ON
        */

        protected static void CreateMenu()
        {
            menu = new Menu("ElRengar", "ElRengar", true);
            Bootstrap.Init(new string[] { }); //blabla

            var comboMenu = new Menu("combo.settings", "Combo settings");
            {
                comboMenu.Add(new MenuSeparator("General", "General"));
                comboMenu.Add(new MenuBool("combo.spell.q", "Use Q", true));
                comboMenu.Add(new MenuBool("combo.spell.w", "Use W", true));
                comboMenu.Add(new MenuBool("combo.spell.e", "Use E", true));
                comboMenu.Add(new MenuBool("combo.spell.e.outofrange", "Use E out of range"));
                comboMenu.Add(new MenuSeparator("Miscellaneous", "Miscellaneous"));
                comboMenu.Add(new MenuBool("combo.spell.ignite", "Use ignite", true));
                comboMenu.Add(new MenuBool("combo.spell.smite", "Use smite", true));
                comboMenu.Add(new MenuSeparator("Prioritized", "Prioritized"));
                comboMenu.Add(new MenuList<string>("combo.prioritize", "Prioritized spell", new[] { "Q", "W", "E" }));
                comboMenu.Add(new MenuSeparator("God modes", "God modes"));
                comboMenu.Add(new MenuKeyBind("combo.spell.triple.q", "Triple Q", Keys.I, KeyBindType.Press));
                comboMenu.Add(new MenuKeyBind("combo.spell.triple.e", "Triple E", Keys.U, KeyBindType.Press));

                menu.Add(comboMenu);
            }

            var harassMenu = new Menu("harass.settings", "Harass settings");
            {
                harassMenu.Add(new MenuSeparator("General", "General"));
                harassMenu.Add(new MenuBool("harass.spell.q", "Use Q", true));
                harassMenu.Add(new MenuBool("harass.spell.w", "Use W", true));
                harassMenu.Add(new MenuBool("harass.spell.e", "Use E", true));
                harassMenu.Add(new MenuSeparator("Prioritized", "Prioritized"));
                harassMenu.Add(new MenuList<string>("harass.prioritize", "Prioritized spell", new[] { "Q", "W", "E" }));

                menu.Add(harassMenu);
            }

            var laneclearMenu = new Menu("laneclear.settings", "Laneclear settings");
            {
                laneclearMenu.Add(new MenuSeparator("General", "General"));
                laneclearMenu.Add(new MenuBool("laneclear.spell.q", "Use Q", true));
                laneclearMenu.Add(new MenuBool("laneclear.spell.w", "Use W", true));
                laneclearMenu.Add(new MenuBool("laneclear.spell.e", "Use E", false));
                laneclearMenu.Add(new MenuSeparator("Items", "Item usage"));
                laneclearMenu.Add(new MenuBool("laneclear.items.hydra", "Ravenous Hydra", true));
                laneclearMenu.Add(new MenuSeparator("Ferocity", "Ferocity"));
                laneclearMenu.Add(new MenuBool("laneclear.save.ferocity", "Save ferocity", true));
                laneclearMenu.Add(new MenuList<string>("laneclear.prioritize", "Prioritized spell", objects: new[] { "Q", "W", "E" }));

                menu.Add(laneclearMenu);
            }

            var jungleClearMenu = new Menu("jungleclear.settings", "Jungleclear settings");
            {
                jungleClearMenu.Add(new MenuSeparator("General", "General"));
                jungleClearMenu.Add(new MenuBool("jungleclear.spell.q", "Use Q", true));
                jungleClearMenu.Add(new MenuBool("jungleclear.spell.w", "Use W", true));
                jungleClearMenu.Add(new MenuBool("jungleclear.spell.e", "Use E", false));
                jungleClearMenu.Add(new MenuSeparator("Items", "Item usage"));
                jungleClearMenu.Add(new MenuBool("jungleclear.items.hydra", "Ravenous Hydra", true));
                jungleClearMenu.Add(new MenuSeparator("Ferocity", "Ferocity"));
                jungleClearMenu.Add(new MenuBool("jungleclear.save.ferocity", "Save ferocity", true));
                jungleClearMenu.Add(new MenuList<string>("jungleclear.prioritize", "Prioritized spell", objects: new[] { "Q", "W", "E" }));

                menu.Add(jungleClearMenu);
            }

            var healMenu = new Menu("heal.settings", "Heal settings");
            {
                healMenu.Add(new MenuSeparator("General", "General"));
                healMenu.Add(new MenuBool("heal.activated", "Use W", true));
                healMenu.Add(new MenuSlider("heal.player.hp", "Player health percentage", value: 25, minValue: 1, maxValue: 100));

                menu.Add(healMenu);
            }

            var miscMenu = new Menu("misc.settings", "Misc settings");
            {
                miscMenu.Add(new MenuSeparator("Notifications", "Notifications"));
                miscMenu.Add(new MenuBool("misc.notifications", "Permashow prioritized spell", true));

                miscMenu.Add(new MenuSeparator("Items", "Item usage"));
                miscMenu.Add(new MenuBool("misc.items.tiamat", "Tiamat", true));
                miscMenu.Add(new MenuBool("misc.items.hydra", "Ravenous Hydra", true));
                miscMenu.Add(new MenuBool("misc.items.ghostblade", "Youmuu's Ghostblade", true));
                miscMenu.Add(new MenuBool("misc.items.cutlass", "Bilgewater Cutlass", true));
                miscMenu.Add(new MenuBool("misc.items.botrk", "Blade of the Ruined King", true));

                miscMenu.Add(new MenuSeparator("Drawings", "Drawings"));
                miscMenu.Add(new MenuBool("misc.drawing.deactivate", "Disable all drawings"));
                miscMenu.Add(new MenuBool("misc.drawing.draw.spell.q", "Q range", true));
                miscMenu.Add(new MenuBool("misc.drawing.draw.spell.w", "W range", true));
                miscMenu.Add(new MenuBool("misc.drawing.draw.spell.e", "E range", true));
                miscMenu.Add(new MenuBool("misc.drawing.draw.spell.r", "R range", true));
                miscMenu.Add(new MenuBool("misc.drawing.draw.helper.canjump", "Jump helper", true));


                miscMenu.Add(new MenuSeparator("Debug", "Debug"));
                miscMenu.Add(new MenuBool("misc.debug.active", "Debug", false));

                menu.Add(miscMenu);
            }

            var creditsMenu = new Menu("credits.settings", "Credits");
            {
                creditsMenu.Add(new MenuSeparator("credits.title.1", "if you would like to donate via PayPal:"));
                creditsMenu.Add(new MenuSeparator("credits.title.2", "Info@zavox.nl"));
                creditsMenu.Add(new MenuSeparator("credits.title.3", "  "));
                creditsMenu.Add(new MenuSeparator("credits.title.4", "This assembly is created by:"));
                creditsMenu.Add(new MenuSeparator("credits.title.5", "jQuery"));
                creditsMenu.Add(new MenuSeparator("credits.title.6", " "));
                creditsMenu.Add(new MenuSeparator("credits.title.7",  String.Format("Version: {0}", Rengar.ScriptVersion)));

                menu.Add(creditsMenu);
            }

            menu.Attach();
        }

        #endregion
    }
}
