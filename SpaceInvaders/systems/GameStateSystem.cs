using ECSharp.core;
using SpaceInvaders.nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.systems
{
    class GameStateSystem : Systeme
    {
        private Engine engine;
        private HashSet<Keys> keyPool;
        private EntityFactory ef;
        private readonly Node n = new GameStateNode();
        private LinkedList<Node> lst;

        public GameStateSystem(Engine engine, HashSet<Keys> keyPool, EntityFactory ef)
        {
            n.SetUp();
            this.engine = engine;
            this.keyPool = keyPool;
            this.ef = ef;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst = e.GetNodeList(n); // Should only be one
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst = null;
        }

        public override void update(float time, Graphics g)
        {
            GameStateNode gsn = (GameStateNode) lst.First();
            if (runnable && gsn != null) {
                switch (gsn.gs.state)
                {
                    case Game.State.Begin:
                        // We launch every system
                        break;
                    case Game.State.Pause:
                        if (keyPool.Contains(Keys.P))
                        {
                            // We launch every system
                            gsn.gs.state = Game.State.Play;
                        }
                        break;
                    case Game.State.Play:
                        if (keyPool.Contains(Keys.P))
                        {
                            // We pause useless system
                            gsn.gs.state = Game.State.Pause;
                        }
                        break;
                    case Game.State.Win:
                        gsn.gs.level++; // On passe au level suivant
                        break;
                    case Game.State.Lost:
                        break;

                }
            }
            
        }
    }
}
