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
    class SpaceShipControlSystem : Systeme
    {
        public LinkedList<Node> lst;
        private readonly Node n = new SpaceShipControlNode();
        private HashSet<Keys> keyPool;
        Size size;

        public SpaceShipControlSystem(HashSet<Keys> keyPool, Size s) : base()
        {
            n.SetUp();
            this.keyPool = keyPool;
            size = s;
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
                foreach (Node n in lst)
                {
                    SpaceShipControlNode ssn = (SpaceShipControlNode)n;
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
                }
            }
        }
    }
}
