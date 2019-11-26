using ECSharp.core;
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
        private LinkedList<Node> lst = new LinkedList<Node>();
        private HashSet<Keys> keyPool;
        private EntityFactory ef;
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Properties.Resources.shoot);

        public GunControlSystem(HashSet<Keys> keyPool, EntityFactory ef) : base()
        {
            n.SetUp();
            this.keyPool = keyPool;
            this.ef = ef;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst = e.GetNodeList(n);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst = null;
        }
        

        public override void update(float time, Graphics g)
        {
            if (runnable)
            {
                foreach (Node node in lst)
                {
                    GunControlNode gcn = (GunControlNode)node;
                    if (keyPool.Contains(gcn.gunControl.space) && gcn.gun.shoot)
                    {
                        gcn.gun.shootingPoint = gcn.pos.point + new Vector2D(gcn.display.bitmap.Width / 2, 0) ;
                        sp.Play();
                        gcn.gun.shoot = false;
                        ef.CreateSpaceShipBullet(gcn.gun);
                        keyPool.Remove(gcn.gunControl.space);
                    }
                    
                }
            }
            
        }
    }
}
