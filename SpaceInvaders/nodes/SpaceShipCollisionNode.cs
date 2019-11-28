using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.nodes
{
    class SpaceShipCollisionNode : Node
    {
        public Position pos;
        public SpaceShip spaceship;
        public Display display;

        public SpaceShipCollisionNode() : base() { }

        public SpaceShipCollisionNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            pos = null;
            spaceship = null;
            display = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                pos,
                spaceship,
                display
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            SpaceShipCollisionNode scn = new SpaceShipCollisionNode(e);
            scn.pos = pos;
            scn.spaceship = spaceship;
            scn.display = display;
            return scn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == pos.ClassId)
            {
                pos = (Position)comp;
            }
            else if (comp.ClassId == spaceship.ClassId)
            {
                spaceship = (SpaceShip)comp;
            }
            else if (comp.ClassId == display.ClassId)
            {
                display = (Display)comp;
            }
            else
            {
                throw new Exception("You passed a wrong component to the " + GetType().Name + " class");
            }
        }

        public override void SetUp()
        {
            spaceship = new SpaceShip();
            pos = new Position();
            display = new Display();

        }
    }
}
