using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElFunHouse.Champions.Alistar
{
    using System.Windows.Forms;

    using ElFunHouse.Config;

    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.UI.IMenu.Values;

    using Menu = LeagueSharp.SDK.Core.UI.IMenu.Menu;

    class CreateMenu : Standards
    {

        public static void Load()
        {
            menu = new Menu("ElAlistar", "ElAlistar", true);
            Bootstrap.Init(new string[] { });

            var comboMenu = new Menu("combo.settings", "Combo settings");
            {
                comboMenu.Add(new MenuSeparator("General", "General"));
                comboMenu.Add(new MenuBool("combo.spell.q", "Use Q", true));
                comboMenu.Add(new MenuBool("combo.spell.w", "Use W", true));
                comboMenu.Add(new MenuBool("combo.spell.e", "Use E", true));

                menu.Add(comboMenu);
            }

            var harassMenu = new Menu("harass.settings", "Harass settings");
            {
                harassMenu.Add(new MenuSeparator("General", "General"));
                harassMenu.Add(new MenuBool("harass.spell.q", "Use Q", true));
           
                menu.Add(harassMenu);
            }


            var flashMenu = new Menu("flash.settings", "Flash settings");
            {
                flashMenu.Add(new MenuSeparator("General", "General"));
                flashMenu.Add(new MenuKeyBind("flash.combo", "Flash combo", Keys.T, KeyBindType.Press));

                menu.Add(flashMenu);
            }

            var creditsMenu = new Menu("credits.settings", "Credits");
            {
                creditsMenu.Add(new MenuSeparator("credits.title.1", "if you would like to donate via PayPal:"));
                creditsMenu.Add(new MenuSeparator("credits.title.2", "Info@zavox.nl"));
                creditsMenu.Add(new MenuSeparator("credits.title.3", "  "));
                creditsMenu.Add(new MenuSeparator("credits.title.4", "This assembly is created by:"));
                creditsMenu.Add(new MenuSeparator("credits.title.5", "jQuery"));
                creditsMenu.Add(new MenuSeparator("credits.title.6", " "));
                creditsMenu.Add(new MenuSeparator("credits.title.7", String.Format("Version: 1")));

                menu.Add(creditsMenu);
            }

            menu.Attach();
        }
    }
}
