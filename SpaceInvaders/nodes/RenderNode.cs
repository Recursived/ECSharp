using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.nodes
{
    class RenderNode : Node
    {
        public Display display;
        public Position pos;

        public RenderNode() : base() { }

        public override void disposeNode()
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

        public override void setComponent(Component comp)
        {
            if (comp.ClassId == display.ClassId)
            {
                display = (Display) comp;
            } else if (comp.ClassId == pos.ClassId)
            {
                pos = (Position) comp;
            } else
            {
                throw new Exception("You passed a wrong component to the " + this.GetType().Name + " class");
            }
        }
    }
}
