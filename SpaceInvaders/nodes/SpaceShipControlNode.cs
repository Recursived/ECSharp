using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.nodes
{
    class SpaceShipControlNode : Node
    {
        public Position pos;
        public SpaceShipControl control;
        public Display display;

        public SpaceShipControlNode() : base() { }

        public SpaceShipControlNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            pos = null;
            control = null;
            display = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                pos,
                control,
                display
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            SpaceShipControlNode ssn = new SpaceShipControlNode(e);
            ssn.pos = pos;
            ssn.control = control;
            ssn.display = display;
            return ssn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == pos.ClassId)
            {
                pos = (Position)comp;
            }
            else if (comp.ClassId == control.ClassId)
            {
                control = (SpaceShipControl)comp;
            }
            else if (comp.ClassId == display.ClassId)
            {
                display = (Display)comp;
            }
            else
            {
                throw new Exception("You passed a wrong component to the " + this.GetType().Name + " class");
            }
        }

        public override void SetUp()
        {
            pos = new Position();
            control = new SpaceShipControl(Keys.Left, Keys.Right, new Velocity(50, 0, 0));
            display = new Display(0, 0, 0, null);
        }
    }
}
