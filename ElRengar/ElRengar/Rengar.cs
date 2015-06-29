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
                //Notifications.Add(new Notification("Test", "test"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region Methods

        private static void OnUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Orbwalk:
                    DoCombo();
                    break;
            }
        }

        #endregion

        private static void DoCombo()
        {

        }
    }
}