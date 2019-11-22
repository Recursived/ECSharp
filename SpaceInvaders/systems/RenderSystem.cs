using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.nodes;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.systems
{
    class RenderSystem : Systeme
    {
        public LinkedList<Node> lst;
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
                    Position p = rn.pos;
                    rn.display.x = p.point.x;
                    rn.display.y = p.point.y;
                    rn.display.rotation = p.rotation;
                    if (g != null) { Draw(rn, g); }

                }
            }
            
        }

        private void Draw(RenderNode rn, Graphics graphics)
        {
            graphics.DrawImage(rn.display.bitmap, rn.display.x, rn.display.y);
        }

}
}
