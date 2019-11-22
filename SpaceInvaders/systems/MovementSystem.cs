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
        public LinkedList<Node> lst;
        private readonly Node n = new MovementNode();
        private readonly Size size;

        public MovementSystem(Size s) : base()
        {
            size = s;
            n.SetUp();
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
                    MovementNode mn = (MovementNode)n;
                    Velocity v = mn.vitesse;
                    Position p = mn.pos;
                    Vector2D newv = time * v.speedVect + p.point;
                    if (newv.x < size.Width || newv.x > 0)
                    {
                        p.point = newv;
                    }
                }
            }
            
        }
    }
}
