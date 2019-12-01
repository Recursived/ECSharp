using ECSharp.core;
using SpaceInvaders.nodes;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpaceInvaders.systems
{
    class SpaceShipControlSystem : Systeme
    {
        private LinkedList<Node> lst;
        private LinkedList<Node> lst_bullet;

        private readonly Node n = new SpaceShipControlNode();
        private readonly Node bulletnode = new BulletControlNode();

        private HashSet<Keys> keyPool;
        Size size;

        public SpaceShipControlSystem(HashSet<Keys> keyPool, Size s) : base()
        {
            n.SetUp();
            bulletnode.SetUp();
            this.keyPool = keyPool;
            size = s;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst = e.GetNodeList(n);
            lst_bullet = e.GetNodeList(bulletnode);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst = null;
            lst_bullet = null;
        }

        public override void update(float time, Graphics g)
        {
            if (runnable)
            {

                SpaceShipControlNode ssn = (SpaceShipControlNode)lst.First();
                if (keyPool.Contains(ssn.control.left))
                {
                    float val = ssn.pos.point.x - ssn.control.v.speedVect.x * time;
                    if (val > 0)
                    {
                        ssn.pos.point.x -= ssn.control.v.speedVect.x * time;
                    }
                }
                else if (keyPool.Contains(ssn.control.right))
                {
                    float val = ssn.pos.point.x + ssn.control.v.speedVect.x * time;
                    if (val < size.Width - ssn.display.bitmap.Width * 1.4)
                    {
                        ssn.pos.point.x += ssn.control.v.speedVect.x * time;
                    }
                }


                // We control bullet if they can be controlled
                foreach (Node bullet in lst_bullet)
                {
                    BulletControlNode bcn = (BulletControlNode)bullet;
                    if (bcn.bullet.ally && bcn.bullet.controlled)
                    {
                        if (keyPool.Contains(Keys.Z))
                        {
                            if (bcn.vitesse.speedVect.y > -1000)
                            {
                                bcn.vitesse.speedVect.y--;
                            }

                        }
                        else if (keyPool.Contains(Keys.S))
                        {
                            if (bcn.vitesse.speedVect.y < -1)
                            {
                                bcn.vitesse.speedVect.y++;
                            }


                        }
                        else if (keyPool.Contains(Keys.Q))
                        {
                            if (bcn.pos.point.x > 1)
                            {
                                bcn.pos.point.x -= (float)0.3;
                            }


                        }
                        else if (keyPool.Contains(Keys.D))
                        {
                            if (bcn.pos.point.x < size.Width - 1)
                            {
                                bcn.pos.point.x += (float)0.3;
                            }


                        }
                    }
                }
            }
        }
    }
}
