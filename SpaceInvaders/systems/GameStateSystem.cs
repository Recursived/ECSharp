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
        private Size size;
        private readonly Node n = new GameStateNode();
        private LinkedList<Node> lst;

        

        public GameStateSystem(Engine engine, HashSet<Keys> keyPool, EntityFactory ef, Size size) : base()
        {
            n.SetUp();
            this.engine = engine;
            this.keyPool = keyPool;
            this.ef = ef;
            this.size = size;
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
                        // We display the begin
                        if (g != null)
                        {
                            g.Flush();
                            g.DrawImage(Properties.Resources.start, size.Width /3, size.Height / 4);
                        }
                        
                        if (keyPool.Contains(Keys.S))
                        {
                            // We launch every system
                            ef.CreateDisplaybleEntity(size, gsn.gs.lives);
                            ef.CreateBunkerEntities(size);
                            ef.CreateEnemyEntities(size, gsn.gs);
                            foreach (Systeme sys in engine.GetSystems())
                            {
                                sys.Start();
                            }
                            keyPool.Remove(Keys.S);
                            gsn.gs.state = Game.State.Play;
                        }
                        break;
                    case Game.State.Pause:
                        if (g != null)
                        {
                            g.DrawImage(Properties.Resources.pause, size.Width / 4, size.Height / 4);
                        }

                        if (keyPool.Contains(Keys.P))
                        {
                            // We launch every system
                            foreach (Systeme sys in engine.GetSystems())
                            {
                                sys.Start();
                            }
                            keyPool.Remove(Keys.P);
                            gsn.gs.state = Game.State.Play;
                        }
                        break;
                    case Game.State.Play:
                        if (keyPool.Contains(Keys.P))
                        {
                            //We pause useless system
                            foreach (Systeme sys in engine.GetSystems())
                            {
                                if (sys.ClassId != "GameStateSystem")
                                {
                                    sys.Stop();
                                }
                            }
                            keyPool.Remove(Keys.P);
                            gsn.gs.state = Game.State.Pause;
                        }

                        if (g != null)
                        {
                            g.DrawString("Nombre de vie : " + gsn.gs.lives + " / Points : " + gsn.gs.score + " / Level : " + gsn.gs.level,
                                Game.font,
                                Game.blackBrush,
                                10,
                                size.Height - 50
                                );

                        }

                        if (gsn.gs.lives <= 0)
                        {
                            gsn.gs.state = Game.State.Lost;
                        } else if (gsn.gs.enemiesCount == 0)
                        {
                            gsn.gs.state = Game.State.Win;
                        }
                        break;
                    case Game.State.Win:
                        foreach (Systeme sys in engine.GetSystems())
                        {
                            if (sys.ClassId != "GameStateSystem")
                            {
                                sys.Stop();
                            }
                        }

                        if (g != null)
                        {
                            g.DrawImage(Properties.Resources.you_win, size.Width / 3, size.Height / 3);
                            g.DrawString("Press 'n' to go to the next level",
                                Game.font,
                                Game.blackBrush,
                                size.Width/2,
                                size.Height - 50
                                );
                        }

                        if (keyPool.Contains(Keys.N))
                        {
                            gsn.gs.level++; // next level
                            keyPool.Remove(Keys.N);
                            foreach (Entity ent in ef.GetEntities().ToList())
                            {
                                if (ent.Name != "game")
                                {
                                    ef.removeEntity(ent);
                                }
                            }
                            gsn.gs.state = Game.State.Begin;
                        }
                        break;
                    case Game.State.Lost:
                        
                        if (g != null)
                        {
                            g.DrawImage(Properties.Resources.game_over, size.Width / 4, size.Height / 4);
                            g.DrawString("Press 'r' to restart",
                                Game.font,
                                Game.blackBrush,
                                size.Width / 2,
                                size.Height - 50
                                );
                        }

                        foreach (Systeme sys in engine.GetSystems())
                        {
                            if (sys.ClassId != "GameStateSystem")
                            {
                                sys.Stop();
                            }
                        }

                        if (keyPool.Contains(Keys.R))
                        {
                            gsn.gs.level = 1;
                            gsn.gs.score = 0;
                            gsn.gs.lives = 3;
                            keyPool.Remove(Keys.R);
                            foreach (Entity ent in ef.GetEntities().ToList())
                            {

                                if (ent.Name != "game")
                                {
                                    ef.removeEntity(ent);
                                }
                            }
                            gsn.gs.state = Game.State.Begin;
                        }
                        
                        break;
                    }
                }
            }          
    }
}
