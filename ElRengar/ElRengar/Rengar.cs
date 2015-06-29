namespace ElRengar
{
    using System;

    using ElRengar.Config;

    using LeagueSharp;
    using LeagueSharp.SDK.Core;
    using LeagueSharp.SDK.Core.Enumerations;

    internal class Rengar : Standards
    {
        #region Public Methods and Operators

        public static void OnLoad(object sender, EventArgs e)
        {
            if (ObjectManager.Player.CharData.BaseSkinName != "Rengar")
            {
                Console.WriteLine("Champion is not supported.");
                return;
            }

            try
            {
                CreateMenu();
                Game.OnUpdate += OnUpdate;
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
        }

        #endregion
    }
}