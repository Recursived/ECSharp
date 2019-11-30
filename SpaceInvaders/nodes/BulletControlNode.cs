using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.nodes
{
    class BulletControlNode : Node
    {
        public Bullet bullet;
        public Velocity vitesse;
        public Position pos;

        public BulletControlNode() : base() { }

        public BulletControlNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            bullet = null;
            vitesse = null;
            pos = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                bullet,
                vitesse, 
                pos
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            BulletControlNode bcn = new BulletControlNode(e);
            bcn.vitesse = vitesse;
            bcn.bullet = bullet;
            bcn.pos = pos;
            return bcn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == vitesse.ClassId)
            {
                vitesse = (Velocity)comp;
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
            bullet = new Bullet(1, true);
            vitesse = new Velocity();
            pos = new Position();
        }
    }
}
