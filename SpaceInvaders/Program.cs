using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpaceInvaders
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            SpaceInvaderForm form = new SpaceInvaderForm();
            Application.EnableVisualStyles();
            Application.Run(form);
            Application.SetCompatibleTextRenderingDefault(false);
            
            
        }
    }
}
