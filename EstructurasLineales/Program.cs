using System;
using System.Windows.Forms;
using EstructurasLineales.UI;

namespace EstructurasLineales
{
    internal static class Program
    {
        /// <summary>Punto de entrada de la aplicación Windows Forms.</summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}
