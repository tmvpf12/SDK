namespace ElRengar
{
    using System;

    using ElRengar.Config;

    internal class Rengar : Standards
    {
        #region Public Methods and Operators

        public static void OnLoad(object sender, EventArgs e)
        {
            try
            {
                CreateMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion
    }
}