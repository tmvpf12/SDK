using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElFunHouse.Champions.Fiora
{
    using System.Windows.Forms;

    using ElFunHouse.Config;

    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.UI.IMenu.Values;

    using Menu = LeagueSharp.SDK.Core.UI.IMenu.Menu;

    class CreateMenu : Standards
    {

        /*
            ______     __  ____  _               _____ _  ____     __
            |  _ \ \   / / |  _ \| |        /\   / ____| |/ /\ \   / /
            | |_) \ \_/ /  | |_) | |       /  \ | |    | ' /  \ \_/ / 
            |  _ < \   /   |  _ <| |      / /\ \| |    |  <    \   /  
            | |_) | | |    | |_) | |____ / ____ \ |____| . \    | |   
            |____/  |_|    |____/|______/_/    \_\_____|_|\_\   |_|   
                                                           
            JUST WAITING TILL HE WILL FUCKING START DOING SOMETHING.
            THANK YOU, SOON. THE BEST FIORA EVER. 10 AA PER SEC WITH FIORA
            EXPLOIT.

            :)                                                        
        */

        public static void Load()
        {
            menu = new Menu("FunHouse Fiora", "FunHouse Fiora", true);
            Bootstrap.Init(new string[] { });

            var comboMenu = new Menu("combo.settings", "Combo settings");
            {
                comboMenu.Add(new MenuSeparator("General", "General"));
                comboMenu.Add(new MenuBool("combo.spell.q", "Use Q", true));
                comboMenu.Add(new MenuBool("combo.spell.w", "Use W", true));
                comboMenu.Add(new MenuBool("combo.spell.e", "Use E", true));
                comboMenu.Add(new MenuBool("combo.spell.r", "Use R", true));

                menu.Add(comboMenu);
            }

            menu.Attach();
        }
    }
}
