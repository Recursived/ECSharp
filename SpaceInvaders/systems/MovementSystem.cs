using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.nodes;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.systems
{
    class MovementSystem : Systeme
    {
        private readonly Node mn = new MovementNode();
        private readonly Node gc = new GunControlNode();
        private readonly Node enemynode = new EnemyNode();
        private readonly Node blocknode = new EnemyBlockNode();
        private readonly Node gamenode = new GameStateNode();

        private LinkedList<Node> lstmovement;
        private LinkedList<Node> lstgun;
        private LinkedList<Node> lst_enemies;
        private LinkedList<Node> lst_block;
        private LinkedList<Node> lst_game;


        private readonly Size size;
        private EntityFactory factory;

        public MovementSystem(Size s, EntityFactory f) : base()
        {
            size = s;
            mn.SetUp();
            gc.SetUp();
            enemynode.SetUp();
            blocknode.SetUp();
            factory = f;
        }

        public override void AddIntoEngine(Engine e)
        {
            lstmovement = e.GetNodeList(mn);
            lstgun = e.GetNodeList(gc);
            lst_block = e.GetNodeList(blocknode);
            lst_enemies = e.GetNodeList(enemynode);
            lst_game = e.GetNodeList(gamenode);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lstmovement = null;
            lstgun = null;
            lst_block = null;
            lst_enemies = null;
        }

        public override void update(float time, Graphics g)
        {
            if (runnable)
            {

                #region basics movement
                GunControlNode gunnode = (GunControlNode)lstgun.First();
                foreach (Node n in lstmovement.ToList()) // We transform to list to entity from node
                {
                    MovementNode mn = (MovementNode)n;
                    Velocity v = mn.vitesse;
                    Position p = mn.pos;
                    Vector2D newv = time * v.speedVect + p.point;
                    if (newv.x < size.Width || newv.x > 0)
                    {
                        p.point = newv;
                    }


                    if (newv.y < 0)
                    {
                        factory.removeEntity(mn.entity);
                        gunnode.gun.shoot = true;
                    } else if (newv.y > size.Height)
                    {
                        factory.removeEntity(mn.entity);
                    }
                }
                #endregion

                #region enemies movement
                GameStateNode gstatenode = null;
                EnemyBlockNode eblocknode = null;
                if (lst_game.Count > 0) { gstatenode = (GameStateNode)lst_game.First(); }
                if (lst_block.Count > 0) { eblocknode = (EnemyBlockNode)lst_block.First(); }
                // First we move enemies
                int compteur = 0;
                foreach (Node enemies in lst_enemies)
                {
                    
                    EnemyNode en = (EnemyNode)enemies;
                    switch (eblocknode.block.direction)
                    {
                        case EnemyBlock.Direction.Right:
                            en.pos.point.x += en.enemy.vitesse.x * time + (gstatenode.gs.level * time);
                            break;
                        case EnemyBlock.Direction.Left:
                            en.pos.point.x -= en.enemy.vitesse.x * time + (gstatenode.gs.level * time);
                            break;


                    }

                    if (eblocknode.block.collided ) {
                        if (time == 0)
                        {
                            time = (float)0.005; // In case the draw update happens at impact
                        }
                        en.pos.point.y += en.enemy.vitesse.y * time;
                        en.enemy.vitesse.y += size.Width/10 + (gstatenode.gs.level * time);
                        en.enemy.vitesse.x += 3 * gstatenode.gs.level;


                    }
                }
                if (eblocknode.block.collided) { eblocknode.block.collided = false; }
                updateEnemyBlock(eblocknode);

                #endregion

            }

        }

        private void updateEnemyBlock(EnemyBlockNode block)
        {
            int min_x = int.MaxValue, min_y = int.MaxValue, max_x = 0, max_y = 0;
            foreach (Node enemies in lst_enemies)
            {
                EnemyNode en = (EnemyNode)enemies;
                // min calcul 
                if (min_x > en.pos.point.x)
                {
                    min_x = (int)en.pos.point.x;
                }

                if (min_y > en.pos.point.y)
                {
                    min_y = (int)en.pos.point.y;
                }

                // max calcul
                if (max_x < en.pos.point.x + en.display.bitmap.Width)
                {
                    max_x = (int)en.pos.point.x + en.display.bitmap.Width;
                }

                if (max_y < en.pos.point.y + en.display.bitmap.Height)
                {
                    max_y = (int)en.pos.point.y + en.display.bitmap.Height;
                }

            }

            block.block.upperLeft.x = min_x;
            block.block.upperLeft.y = min_y;
            block.block.bottomRight.x = max_x;
            block.block.bottomRight.y = max_y;


        }
    }
}
