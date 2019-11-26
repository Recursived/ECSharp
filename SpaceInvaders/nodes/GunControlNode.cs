using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.nodes
{
    class GunControlNode : Node
    {
        public GunControl gunControl;
        public Gun gun;
        public Position pos;
        public Display display;

        public GunControlNode() : base() { }

        public GunControlNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            gunControl = null;
            gun = null;
            pos = null;
            display = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                gunControl,
                gun,
                pos,
                display
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            GunControlNode gcn = new GunControlNode(e);
            gcn.pos = pos;
            gcn.gun = gun;
            gcn.gunControl = gunControl;
            gcn.display = display;
            return gcn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == pos.ClassId)
            {
                pos = (Position)comp;
            }
            else if (comp.ClassId == gun.ClassId)
            {
                gun = (Gun)comp;
            }
            else if (comp.ClassId == gunControl.ClassId)
            {
                gunControl = (GunControl)comp;
            } else if (comp.ClassId == display.ClassId)
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
            gunControl = new GunControl(Keys.Space);
            gun = new Gun(0, 0);
            pos = new Position();
            display = new Display();
        }
    }
}
