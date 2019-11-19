using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SpaceInvaders
{
    public partial class SpaceInvaderForm : Form
    {

        private Game game;
        public Graphics graphics;
        Stopwatch watch = new Stopwatch();
        long lastTime = 0;



        public SpaceInvaderForm(Game g)
        {
            InitializeComponent();
            game = g;
            watch.Start();
            WorldClock.Start();

        }

        #region events
        /// <summary>
        /// Paint event of the form, see msdn for help => paint game with double buffering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            Graphics g = bg.Graphics;
            g.Clear(Color.White);
            bg.Render();
            bg.Dispose();

        }


        /// <summary>
        /// Tick event => update game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldClock_Tick(object sender, EventArgs e)
        {
            // lets do 5 ms update to avoid quantum effects
            int maxDelta = 5;

            // get time with millisecond precision
            long nt = watch.ElapsedMilliseconds;
            // compute ellapsed time since last call to update
            double deltaT = (nt - lastTime);

            for (; deltaT >= maxDelta; deltaT -= maxDelta)
                game.Update((int)(maxDelta / 1000.0));

            game.Update((int)(deltaT / 1000.0));

            // remember the time of this update
            lastTime = nt;

            Invalidate();

        }


        private void SIForm_Load(object sender, EventArgs e)
        {
               
        }

        private void SIForm_KeyDown(object sender, KeyEventArgs e)
        {
            game.keyPool.Add(e.KeyCode);
        }

        private void SIForm_KeyUp(object sender, KeyEventArgs e)
        {
            game.keyPool.Remove(e.KeyCode);
        }
    }
}
