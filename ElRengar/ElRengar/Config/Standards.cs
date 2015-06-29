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

        private static Menu menu;

        #endregion

        #region Properties

        public static Obj_AI_Hero Player { get; set; }

        #endregion

        #region Methods

        protected static void CreateMenu()
        {
            Bootstrap.Init(new[] { "" }); //blabla

            menu = new Menu("ElRengar", "ElRengar", true);

            var comboMenu = menu.Add(new Menu("Combo settings", "Combo settings"));
            {
                comboMenu.Add(new MenuSeparator("General", "General"));
                comboMenu.Add(new MenuBool("combo.spell.q", "Use Q", true));
                comboMenu.Add(new MenuBool("combo.spell.w", "Use W", true));
                comboMenu.Add(new MenuBool("combo.spell.e", "Use E", true));
                comboMenu.Add(new MenuBool("combo.spell.e.outofrange", "Use E out of range", false));
                comboMenu.Add(new MenuSeparator("Miscellaneous", "Miscellaneous"));
                comboMenu.Add(new MenuBool("combo.spell.ignite", "Use ignite", true));
                comboMenu.Add(new MenuBool("combo.spell.smite", "Use smite", true));
                comboMenu.Add(new MenuSeparator("Prioritized", "Prioritized"));
                comboMenu.Add(new MenuList<string>("combo.prioritze", "Prioritized spell", new[] { "Q", "W", "E" }));
                comboMenu.Add(new MenuSeparator("God mode", "God mode"));
                comboMenu.Add(new MenuKeyBind("combo.spell.triple.q", "Triple Q", Keys.T, KeyBindType.Press));
            }

            var harassMenu = menu.Add(new Menu("Harass settings", "Harass settings"));
            {
                harassMenu.Add(new MenuSeparator("General", "General"));
                harassMenu.Add(new MenuBool("harass.spell.q", "Use Q", true));
                harassMenu.Add(new MenuBool("harass.spell.w", "Use W", true));
                harassMenu.Add(new MenuBool("harass.spell.e", "Use E", true));
                harassMenu.Add(new MenuSeparator("Prioritized", "Prioritized"));
                harassMenu.Add(new MenuList<string>("harass.prioritze", "Prioritized spell", new[] { "Q", "W", "E" }));
            }

            menu.Attach();
        }

        #endregion
    }
}