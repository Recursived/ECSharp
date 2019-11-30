using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.nodes;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.systems
{
    class GunControlSystem : Systeme
    {
        private readonly Node n = new GunControlNode();
        private readonly Node enemynode = new EnemyNode();
        private readonly Node blocknode = new EnemyBlockNode();
        private readonly Node gamenode = new GameStateNode();


        private LinkedList<Node> lst = new LinkedList<Node>();
        private LinkedList<Node> lst_enemies = new LinkedList<Node>();
        private LinkedList<Node> lst_block = new LinkedList<Node>();
        private LinkedList<Node> lst_game = new LinkedList<Node>();



        private HashSet<Keys> keyPool;
        private EntityFactory ef;
        private int timer_bonus = 0;
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Properties.Resources.shoot);

        public GunControlSystem(HashSet<Keys> keyPool, EntityFactory ef) : base()
        {
            n.SetUp();
            gamenode.SetUp();
            enemynode.SetUp();
            blocknode.SetUp();
            this.keyPool = keyPool;
            this.ef = ef;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst = e.GetNodeList(n);
            lst_enemies = e.GetNodeList(enemynode);
            lst_block = e.GetNodeList(blocknode);
            lst_game = e.GetNodeList(gamenode);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst = null;
            lst_enemies = null;
            lst_block = null;
            lst_game = null;
        }
        

        public override void update(float time, Graphics g)
        {
            if (runnable)
            {
                EnemyBlockNode ben = (EnemyBlockNode)lst_block.First();
                GameStateNode gsn = (GameStateNode)lst_game.First();
                foreach (Node node in lst)
                {
                    GunControlNode gcn = (GunControlNode)node;
                    if (keyPool.Contains(gcn.gunControl.space) && gcn.gun.shoot)
                    {
                        gcn.gun.shootingPoint = gcn.pos.point + new Vector2D(gcn.display.bitmap.Width / 2, 0);
                        sp.Play();
                        gcn.gun.shoot = false;
                        keyPool.Remove(gcn.gunControl.space);

                        if (gcn.gun.doubleShoot && timer_bonus < 5)
                        {
                            timer_bonus++;
                        } else
                        {
                            
                            timer_bonus = 0;
                            gcn.gun.doubleShoot = false;
                        }
                        ef.CreateSpaceShipBullet(gcn.gun);

                    }

                }



                // we update enemies gun shooting point
                foreach (Node enemy in lst_enemies)
                {
                    EnemyNode en = (EnemyNode)enemy;
                    en.gun.shootingPoint = en.pos.point + new Vector2D(en.display.bitmap.Width / 2, 0);
                }

                // we see if we shoot anything, if so we choose a random enemy to do so
                if (Game.rand.Next(ben.block.shootProbability) <= gsn.gs.level*2)
                {
                    int index = Game.rand.Next(lst_enemies.Count);
                    EnemyNode en = (EnemyNode)lst_enemies.ToList()[index];
                    int damage = 0;
                    Bitmap b = null;
                    switch (en.enemy.type)
                    {
                        case Enemy.Type.Big:
                            b = Properties.Resources.shoot4;
                            damage = 3 + gsn.gs.level;
                            break;
                        case Enemy.Type.Medium:
                            damage = 2 + gsn.gs.level;
                            b = Properties.Resources.shoot3;
                            break;
                        case Enemy.Type.Small:
                            b = Properties.Resources.shoot2;
                            damage = 1 + gsn.gs.level;
                            break;
                    }
                    ef.CreateEnemyBullet(en.gun, b, damage);
                }
            }
            
        }
    }
}
