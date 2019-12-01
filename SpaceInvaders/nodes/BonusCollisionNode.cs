using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.nodes
{
    class BonusCollisionNode : Node
    {
        public Bonus bonus;
        public Position pos;
        public Display display;

        public BonusCollisionNode() : base() { }

        public BonusCollisionNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            bonus = null;
            pos = null;
            display = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                bonus,
                pos,
                display
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            BonusCollisionNode bcn = new BonusCollisionNode(e);
            bcn.bonus = bonus;
            bcn.pos = pos;
            bcn.display = display;
            return bcn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == display.ClassId)
            {
                display = (Display)comp;
            }
            else if (comp.ClassId == bonus.ClassId)
            {
                bonus = (Bonus)comp;
            }
            else if (comp.ClassId == pos.ClassId)
            {
                pos = (Position)comp;
            }
            else
            {
                throw new Exception("You passed a wrong component to the " + GetType().Name + " class");
            }
        }

        public override void SetUp()
        {
            bonus = new Bonus();
            pos = new Position();
            display = new Display();
        }
    }
}
