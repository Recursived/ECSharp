using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using ECSharp.core;
using SpaceInvaders.systems;

namespace SpaceInvaders
{
    public class Game
    {
        private Engine e;
        private EntityFactory factory;
        public Form container;
        Size size;
        public HashSet<Keys> keyPool = new HashSet<Keys>();

        public Game(Form container)
        {
            this.container = container;
            init();
        }

        private void init()
        {
            e = new Engine();
            factory = new EntityFactory(e);
            Size size = container.Size;

            e.AddSystem(new RenderSystem(container.CreateGraphics()), Systeme.Priority.Render);


        }

        public void Update(int time)
        {
            e.update(time);
        }

        private ReleaseKey(Keys k) { keyPool.Remove(k); }
    }
}
