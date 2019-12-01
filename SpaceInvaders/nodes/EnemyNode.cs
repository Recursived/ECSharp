using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.nodes
{
    class EnemyNode : Node
    {
        public Position pos;
        public Enemy enemy;
        public Gun gun;
        public Display display;

        public EnemyNode() : base() { }

        public EnemyNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            pos = null;
            enemy = null;
            gun = null;
            display = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                pos,
                enemy,
                gun,
                display
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            EnemyNode en = new EnemyNode(e);
            en.pos = pos;
            en.enemy = enemy;
            en.gun = gun;
            en.display = display;
            return en;
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
            else if (comp.ClassId == enemy.ClassId)
            {
                enemy = (Enemy)comp;
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
            pos = new Position();
            gun = new Gun(0, 0);
            enemy = new Enemy(1, Enemy.Type.Medium, 0, 0);
            display = new Display();

        }
    }
}
