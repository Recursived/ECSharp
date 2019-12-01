using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class SpaceInvaderForm : Form
    {

        private Game game;

        Stopwatch watch = new Stopwatch();
        long lastTime = 0;

        private Graphics g;
        public BufferedGraphics bg;

        private bool isKeyDown = false;


        public SpaceInvaderForm()
        {
            InitializeComponent();
            game = new Game(this);
            watch.Start();
            WorldClock.Start();

        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            g = bg.Graphics;
            g.Clear(Color.White);
            game.Update(0, g);
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
                game.Update((float)(maxDelta / 1000.0));

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
            if (isKeyDown && e.KeyCode == Keys.Space)
            {
                return;
            }
            isKeyDown = true;
            game.keyPool.Add(e.KeyCode);
        }

        private void SIForm_KeyUp(object sender, KeyEventArgs e)
        {
            isKeyDown = false;
            game.keyPool.Remove(e.KeyCode);
        }
    }
}
