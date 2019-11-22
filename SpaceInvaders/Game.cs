using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using ECSharp.core;
using SpaceInvaders.systems;
using System.Windows.Media;

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
        private MediaPlayer mp = new MediaPlayer();
        public enum State
        {
            Begin,
            Play,
            Pause,
            Win,
            Lost
        }


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

            e.AddSystem(new SpaceShipControlSystem(keyPool, size), Systeme.Priority.Update);
            e.AddSystem(new GunControlSystem(keyPool, factory), Systeme.Priority.Update);
            e.AddSystem(new MovementSystem(size), Systeme.Priority.Move);
            e.AddSystem(new RenderSystem(), Systeme.Priority.Render);


            // We start every system so that they can be updated
            foreach (Systeme sys in e.GetSystems())
            {
                sys.Start();
            }

            Entity spaceship = factory.CreateDisplaybleEntity(size);
            
            factory.CreateBunkerEntities(size);
            


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="g">Si graphics est null, on update sinon il s'agit d'un draw</param>
        public void Update(float time, Graphics g = null)
        {
            e.Update(time, g);
        }

        private void ReleaseKey(Keys k) { keyPool.Remove(k); }
    }
}
