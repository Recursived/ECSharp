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
    class CollisionSystem : Systeme
    {

        // TO-DO : ajouter une node pour les enemis et le vaisse du joueur ainsi que la liste
        #region fields
        private readonly Node bunkernode = new BunkerCollisionNode();
        private readonly Node bulletnode = new BulletCollisionNode();
        private readonly Node enemynode = new EnemyNode();
        private readonly Node spaceshipnode = new SpaceShipCollisionNode();
        private readonly Node gunnode = new GunControlNode();
        private readonly Node gamenode = new GameStateNode();
        public readonly Node bonusnode = new BonusCollisionNode();

        private LinkedList<Node> lst_bunker;
        private LinkedList<Node> lst_bullet;
        private LinkedList<Node> lst_gun;
        private LinkedList<Node> lst_spaceship;
        private LinkedList<Node> lst_enemy;
        private LinkedList<Node> lst_game;
        private LinkedList<Node> lst_bonus;

        private EntityFactory ef;
        private Size size;
        private int pixel_destroyed = 0;
        private System.Media.SoundPlayer bunk_expl = new System.Media.SoundPlayer(Properties.Resources.bunker_col);
        #endregion

        #region method and constructor
        public CollisionSystem(EntityFactory ef, Size s) : base()
        {
            bunkernode.SetUp();
            bulletnode.SetUp();
            spaceshipnode.SetUp();
            enemynode.SetUp();
            gunnode.SetUp();
            gamenode.SetUp();
            bonusnode.SetUp();
            size = s;
            this.ef = ef;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst_bullet = e.GetNodeList(bulletnode);
            lst_bunker = e.GetNodeList(bunkernode);
            lst_gun = e.GetNodeList(gunnode);
            lst_spaceship = e.GetNodeList(spaceshipnode);
            lst_enemy = e.GetNodeList(enemynode);
            lst_game = e.GetNodeList(gamenode);
            lst_bonus = e.GetNodeList(bonusnode);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst_bunker = null;
            lst_bullet = null;
            lst_gun = null;
            lst_spaceship = null;
            lst_enemy = null;
            lst_bonus = null;
            lst_game = null;
        }

        #endregion

        public override void update(float time, Graphics g)
        {
            // We set up the gunnode to allow it to shoot again
            GunControlNode gunctrlnode = null;
            GameStateNode gstatenode = null;
            SpaceShipCollisionNode sscn = null;
            if (lst_gun.Count > 0) { gunctrlnode = (GunControlNode)lst_gun.First(); }
            if (lst_game.Count > 0) { gstatenode = (GameStateNode)lst_game.First(); }

            #region bullet bunker collision
            foreach (Node bullet in lst_bullet.ToList()) // to list to remove entity while looping
            {
                foreach (Node bunker in lst_bunker.ToList())
                {
                    BulletCollisionNode bullcn = (BulletCollisionNode)bullet;
                    BunkerCollisionNode bunkcn = (BunkerCollisionNode)bunker;

                    if (g != null)
                    {
                        g.DrawRectangle(new Pen(Brushes.Red), new Rectangle((int)bunkcn.pos.point.x, (int)bunkcn.pos.point.y, bunkcn.display.bitmap.Width, bunkcn.display.bitmap.Height));
                    }

                    if (bunkcn.bunker.life > 0 &&
                        CheckRectangleCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos) &&
                        CheckPixelCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos)
                        )
                    {
                        CheckPixelCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos, bullcn.bullet.damage);
                        ef.removeEntity(bullcn.entity);
                        bunkcn.bunker.life -= pixel_destroyed;
                        pixel_destroyed = 0;
                        if (gunctrlnode != null) { gunctrlnode.gun.shoot = true; }
                    }
                }
            }
            #endregion

            #region enemy bullet collision
            foreach (Node bullet in lst_bullet.ToList()) // to list to remove entity while looping
            {
                foreach (Node enemy in lst_enemy.ToList())
                {
                    BulletCollisionNode bullcn = (BulletCollisionNode)bullet;
                    EnemyNode en = (EnemyNode)enemy;

                    if (g != null)
                    {
                        g.DrawRectangle(new Pen(Brushes.Red), new Rectangle((int)en.pos.point.x, (int)en.pos.point.y, en.display.bitmap.Width, en.display.bitmap.Height));
                    }

                    if (en.enemy.life > 0 &&
                        CheckRectangleCollision(bullcn.display, bullcn.pos, en.display, en.pos) &&
                        CheckPixelCollision(bullcn.display, bullcn.pos, en.display, en.pos)
                        )
                    {
                        CheckPixelCollision(bullcn.display, bullcn.pos, en.display, en.pos);
                        ef.removeEntity(bullcn.entity);
                        en.enemy.life -= 1;
                        if (en.enemy.life <= 0)
                        {
                            if (gstatenode != null)
                            {
                                switch (en.enemy.type)
                                {
                                    case Enemy.Type.Big:
                                        gstatenode.gs.score += 500;
                                        break;
                                    case Enemy.Type.Medium:
                                        gstatenode.gs.score += 300;
                                        break;
                                    case Enemy.Type.Small:
                                        gstatenode.gs.score += 100;
                                        break;
                                }
                            }
                            ef.removeEntity(en.entity);
                        }
                        pixel_destroyed = 0;
                        if (gunctrlnode != null) { gunctrlnode.gun.shoot = true; }
                    }
                }
            }
            #endregion

            #region spaceship bullet collision
            if (lst_spaceship.Count > 0)
            {
                sscn = (SpaceShipCollisionNode)lst_spaceship.First();
                foreach (Node bullet in lst_bullet.ToList())
                {
                    BulletCollisionNode bullcn = (BulletCollisionNode)bullet;
                    if (g != null)
                    {
                        g.DrawRectangle(new Pen(Brushes.Red), new Rectangle((int)sscn.pos.point.x, (int)sscn.pos.point.y, sscn.display.bitmap.Width, sscn.display.bitmap.Height));
                    }

                    if (sscn.spaceship.life > 0 &&
                        CheckRectangleCollision(bullcn.display, bullcn.pos, sscn.display, sscn.pos) &&
                        CheckPixelCollision(bullcn.display, bullcn.pos, sscn.display, sscn.pos)
                        )
                    {
                        CheckPixelCollision(bullcn.display, bullcn.pos, sscn.display, sscn.pos, bullcn.bullet.damage);
                        ef.removeEntity(bullcn.entity);
                        sscn.spaceship.life -= 1;
                        gstatenode.gs.lives = sscn.spaceship.life;
                        pixel_destroyed = 0;
                    }
                    
                }
            }
            #endregion

            #region spaceship bonus collision
            if (lst_spaceship.Count > 0)
            {
                sscn = (SpaceShipCollisionNode)lst_spaceship.First();
                foreach (Node bonus in lst_bonus.ToList())
                {
                    BonusCollisionNode bonusn = (BonusCollisionNode)bonus;
                    if (g != null)
                    {
                        g.DrawRectangle(new Pen(Brushes.Red), new Rectangle((int)sscn.pos.point.x, (int)sscn.pos.point.y, sscn.display.bitmap.Width, sscn.display.bitmap.Height));
                    }

                    if (sscn.spaceship.life > 0 &&
                        CheckRectangleCollision(bonusn.display, bonusn.pos, sscn.display, sscn.pos) &&
                        CheckPixelCollision(bonusn.display, bonusn.pos, sscn.display, sscn.pos)
                        )
                    {
                        CheckPixelCollision(bonusn.display, bonusn.pos, sscn.display, sscn.pos);
                        switch (bonusn.bonus.type)
                        {
                            case Bonus.Type.PlusLife:
                                sscn.spaceship.life++;
                                break;
                            case Bonus.Type.ScoreBoost:
                                gstatenode.gs.score *= 2;
                                break;
                            case Bonus.Type.RestoreBunker:
                                    foreach (Node bunker in lst_bunker.ToList())
                                    {
                                        ef.removeEntity(bunker.entity);
                                    }
                                    ef.CreateBunkerEntities(size);
                                break;
                        }
                        ef.removeEntity(bonusn.entity);
                        gstatenode.gs.lives = sscn.spaceship.life;
                        pixel_destroyed = 0;
                    }

                }
            }
            #endregion
        }


        #region collision methods
        private bool CheckRectangleCollision(Display d1, Position p1, Display d2, Position p2) 
        {
            if (p1.point.x + d1.bitmap.Width >= p2.point.x &&
                p1.point.x <= p2.point.x + d2.bitmap.Width &&
                p1.point.y + d1.bitmap.Height >= p2.point.y &&
                p1.point.y <= p2.point.y + d2.bitmap.Height)
            {
                return true;
            }
            return false;
        }

        private bool CheckPixelCollision(Display d1, Position p1, Display d2, Position p2, int damage = 0)
        {
            float x1 = Math.Max(p1.point.x, p2.point.x);
            float x2 = Math.Min(p1.point.x + d1.bitmap.Width, p2.point.x + d2.bitmap.Width);

            float y1 = Math.Max(p1.point.y, p2.point.y);
            float y2 = Math.Min(p1.point.y + d1.bitmap.Height, p2.point.y + d2.bitmap.Height);

            // For each single pixel in the intersecting rectangle
            for (int y = (int)y1; y < y2; ++y)
            {
                for (int x = (int)x1; x < x2; ++x)
                {
                    if (d1.bitmap.GetPixel((int)(x - p1.point.x),(int) (y - p1.point.y)) == Color.FromArgb(255, 0, 0, 0) &&
                        d1.bitmap.GetPixel((int)(x - p1.point.x),(int) (y - p1.point.y)) ==
                        d2.bitmap.GetPixel((int)(x - p2.point.x), (int)(y - p2.point.y)))
                    {
                        if (damage > 0) { DestroyPixel(d2, (int)(x - p2.point.x), (int)(y - p2.point.y), damage); }
                        bunk_expl.Play();
                        return true;
                    }
                }
            }
            return false;
        }

        private void DestroyPixel(Display d, int x, int y, int damage)
        {
            try
            {
                d.bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
                for (int i = -damage*5; i < damage*4; i++)
                {
                    for (int j = -damage*5; j < damage*4; j++)
                    {
                        if (Math.Abs(i) + Math.Abs(j) < 4)
                        {
                            d.bitmap.SetPixel(x + i, y + j, Color.FromArgb(0, 255, 255, 255));
                            pixel_destroyed++;
                        }
                        
                    }
                }

            }
            catch (Exception e)
            {
                // We are out of the bitmap so we do nothing
            }
        }
        #endregion
    }
}
