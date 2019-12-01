using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.nodes
{
    class MovementNode : Node
    {
        public Position pos;
        public Velocity vitesse;

        public MovementNode() : base() { }

        public MovementNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            pos = null;
            vitesse = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                vitesse,
                pos
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            MovementNode mn = new MovementNode(e);
            mn.vitesse = this.vitesse;
            mn.pos = this.pos;
            return mn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == vitesse.ClassId)
            {
                vitesse = (Velocity)comp;
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
            vitesse = new Velocity();
            pos = new Position();
        }
    }
}
