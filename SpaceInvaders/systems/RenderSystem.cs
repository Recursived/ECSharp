using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.nodes;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.systems
{
    class RenderSystem : Systeme
    {
        public Graphics graphics;
        public LinkedList<Node> lst;
        private readonly Node n = new RenderNode();

        public RenderSystem(Graphics g) : base()
        {
            graphics = g;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst = e.GetNodeList(n);
            foreach (Node n in lst)
            {
                RenderNode rn = (RenderNode)n;
                graphics.DrawImage(rn.display.bitmap, rn.display.x, rn.display.y);
            }
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst = null;
        }

        public override void update(int time)
        {
            foreach (Node n in lst)
            {
                RenderNode rn = (RenderNode) n;
                Position p = rn.pos;
                rn.display.x = p.point.x;
                rn.display.y = p.point.y;
                rn.display.rotation = p.rotation;

            }
        }
    }
}
