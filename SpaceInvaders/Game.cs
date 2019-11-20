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
        public Graphics graphics;

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

            e.AddSystem(new RenderSystem(), Systeme.Priority.Render);

            factory.CreateDisplaybleEntity(size);
            factory.CreateBunkerEntities(size);

            
            


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="g">Si graphics est null, on update sinon il s'agit d'un draw</param>
        public void Update(int time, Graphics g = null)
        {
            e.Update(time, g);
        }

        private void ReleaseKey(Keys k) { keyPool.Remove(k); }
    }
}
