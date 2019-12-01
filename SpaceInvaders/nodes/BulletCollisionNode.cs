using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.nodes
{
    class BulletCollisionNode : Node
    {
        public Bullet bullet;
        public Position pos;
        public Display display;

        public BulletCollisionNode() : base() { }

        public BulletCollisionNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            bullet = null;
            pos = null;
            display = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                bullet,
                pos,
                display
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            BulletCollisionNode bcn = new BulletCollisionNode(e);
            bcn.display = display;
            bcn.bullet = bullet;
            bcn.pos = pos;
            return bcn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == display.ClassId)
            {
                display = (Display)comp;
            }
            else if (comp.ClassId == bullet.ClassId)
            {
                bullet = (Bullet)comp;
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
            display = new Display();
            bullet = new Bullet(1, true);
            pos = new Position();
        }
    }
}
