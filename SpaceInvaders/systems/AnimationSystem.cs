using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.systems
{
    class AnimationSystem : Systeme
    {
        private readonly AnimationNode n = new AnimationNode();
        private LinkedList<Node> lst;

        public AnimationSystem() : base()
        {
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
            // TO-DO : maj des positions du bitmap avec le composant position
            if (runnable)
            {
                foreach (Node n in lst)
                {
                    AnimationNode rn = (AnimationNode)n;
                    if (g != null) { Draw(rn, g); }

                }
            }
        }

        private void Draw(AnimationNode rn, Graphics graphics)
        {
            graphics.DrawImage(rn.anim.bitmapanimation.GetNextFrame(), rn.pos.point.x, rn.pos.point.y);
        }
    }
}
