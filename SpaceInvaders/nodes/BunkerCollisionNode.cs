using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.nodes
{
    class BunkerCollisionNode : Node
    {
        public Bunker bunker;
        public Position pos;
        public Display display;

        public BunkerCollisionNode() : base() { }

        public BunkerCollisionNode(Entity e) : base(e) { } 
        public override void DisposeNode()
        {
            bunker = null;
            display = null;
            pos = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                display,
                pos,
                bunker
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            BunkerCollisionNode bn = new BunkerCollisionNode(e);
            bn.display = display;
            bn.bunker = bunker;
            bn.pos = pos;
            return bn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == display.ClassId)
            {
                display = (Display)comp;
            }
            else if (comp.ClassId == bunker.ClassId)
            {
                bunker = (Bunker)comp;
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
            bunker = new Bunker();
            display = new Display();
            pos = new Position();
        }
    }
}
