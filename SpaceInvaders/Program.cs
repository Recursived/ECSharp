﻿using System;
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
            try
            {
                SpaceInvaderForm form = new SpaceInvaderForm();
                Application.EnableVisualStyles();
                Application.Run(form);
                Application.SetCompatibleTextRenderingDefault(false);
            } catch(Exception e)
            {

            }
            


        }
    }
}
