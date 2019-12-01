using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.nodes
{
    class RenderNode : Node
    {
        public Display display;
        public Position pos;

        public RenderNode() : base() { }

        public RenderNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            display = null;
            pos = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                display,
                pos
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            RenderNode rn = new RenderNode(e);
            rn.display = this.display;
            rn.pos = this.pos;
            return rn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == display.ClassId)
            {
                display = (Display)comp;
            }
            else if (comp.ClassId == pos.ClassId)
            {
                pos = (Position)comp;
            }
            else
            {
                throw new Exception("You passed a wrong component to the " + this.GetType().Name + " class");
            }
        }

        public override void SetUp()
        {
            display = new Display();
            pos = new Position();

        }
    }
}
