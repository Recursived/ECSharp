using ECSharp.core;
using SpaceInvaders.nodes;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.systems
{
    class RenderSystem : Systeme
    {
        private LinkedList<Node> lst;
        private readonly Node n = new RenderNode();

        public RenderSystem() : base()
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
            if (runnable)
            {
                foreach (Node n in lst)
                {
                    RenderNode rn = (RenderNode)n;
                    if (g != null) { Draw(rn, g); }

                }
            }

        }

        private void Draw(RenderNode rn, Graphics graphics)
        {
            graphics.DrawImage(rn.display.bitmap, rn.pos.point.x, rn.pos.point.y);
        }

    }
}
