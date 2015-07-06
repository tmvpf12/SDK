namespace ElFunHouse.Config
{
    using System;
    using System.Runtime.InteropServices.WindowsRuntime;
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

    public class Standards
    {
        public enum Spells
        {
            Q,
            W,
            E,
            R
        }

        public static Menu menu;

        public static Obj_AI_Hero Player
        {
            get
            {
                return ObjectManager.Player;
            }
        }

        public static OrbwalkerMode ActiveMode
        {
            get; set;
        }

        public static int Mana
        {
            get
            {
                return (int)ObjectManager.Player.Mana;
            }
        }
    }
}
