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

    using System.Windows.Forms;

    using LeagueSharp;
    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.UI.IMenu.Values;

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

        public static bool IsVisible = true;

        public static int LeapRange = 775;

        public static Menu menu;

        public static OrbwalkerMode ActiveMode { get; set; }

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

        public static bool RengarR
        {
            get
            {
                return Player.HasBuff("rengarr");
            }
        }

        #endregion

        #region Methods

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
                comboMenu.Add(new MenuSeparator("God mode", "God mode"));
                comboMenu.Add(new MenuKeyBind("combo.spell.triple.q", "Triple Q", Keys.T, KeyBindType.Press));

                menu.Add(comboMenu);
            }

            var harassMenu = new Menu("harass.settings", "Harass settings");
            {
                harassMenu.Add(new MenuSeparator("General", "General"));
                harassMenu.Add(new MenuBool("harass.spell.q", "Use Q", true));
                harassMenu.Add(new MenuBool("harass.spell.w", "Use W", true));
                harassMenu.Add(new MenuBool("harass.spell.e", "Use E", true));
                harassMenu.Add(new MenuSeparator("Prioritized", "Prioritized"));
                harassMenu.Add(new MenuList<string>("harass.prioritze", "Prioritized spell", new[] { "Q", "W", "E" }));

                menu.Add(harassMenu);
            }

            var drawingsMenu = new Menu("drawing.settings", "Draw settings");
            {
                drawingsMenu.Add(new MenuSeparator("General", "General"));
                drawingsMenu.Add(new MenuBool("drawing.deactivate", "Disable all drawings"));
                drawingsMenu.Add(new MenuBool("drawing.draw.spell.w", "W range", true));
                drawingsMenu.Add(new MenuBool("drawing.draw.spell.e", "E range", true));
                drawingsMenu.Add(new MenuBool("drawing.draw.spell.r", "R range", true));

                menu.Add(drawingsMenu);
            }

            menu.Attach();
        }

        #endregion
    }
}