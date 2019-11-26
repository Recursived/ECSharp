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
        private readonly Node bunkernode = new BunkerCollisionNode();
        private readonly Node bulletnode = new BulletCollisionNode();
        private readonly Node gunnode = new GunControlNode();
        private LinkedList<Node> lst_bunker;
        private LinkedList<Node> lst_bullet;
        private LinkedList<Node> lst_gun;
        private EntityFactory ef;
        private System.Media.SoundPlayer bunk_expl = new System.Media.SoundPlayer(Properties.Resources.bunker_col);


        public CollisionSystem(EntityFactory ef) : base()
        {
            bunkernode.SetUp();
            bulletnode.SetUp();
            gunnode.SetUp();
            this.ef = ef;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst_bullet = e.GetNodeList(bulletnode);
            lst_bunker = e.GetNodeList(bunkernode);
            lst_gun = e.GetNodeList(gunnode);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst_bunker = null;
            lst_bullet = null;
            lst_gun = null;
        }

        public override void update(float time, Graphics g)
        {
            GunControlNode gunctrlnode = null;
            if (lst_gun.Count > 0) { gunctrlnode = (GunControlNode)lst_gun.First(); }
            foreach (Node bullet in lst_bullet.ToList()) // to list to remove entity while looping
            {
                foreach(Node bunker in lst_bunker.ToList())
                {
                    BulletCollisionNode bullcn = (BulletCollisionNode)bullet;
                    BunkerCollisionNode bunkcn = (BunkerCollisionNode)bunker;
                    if (g != null) { 
                        g.DrawRectangle(new Pen(Brushes.Red), new Rectangle((int)bunkcn.pos.point.x, (int)bunkcn.pos.point.y, bunkcn.display.bitmap.Width, bunkcn.display.bitmap.Height)); 
                    }
                    if (bunkcn.bunker.life > 0 && 
                        CheckRectangleCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos) &&
                        CheckPixelCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos)
                        )
                    {
                        CheckPixelCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos, bullcn.bullet.damage);
                        ef.removeEntity(bullcn.entity);
                        if (gunctrlnode != null) { gunctrlnode.gun.shoot = true; }
                    }
                }
            }
        }

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
                        }
                        
                    }
                }

            }
            catch (Exception e)
            {
                // We are out of the bitmap so we do nothing
            }
        }
    }
}
