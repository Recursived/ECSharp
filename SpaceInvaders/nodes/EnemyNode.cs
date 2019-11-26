using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            throw new NotImplementedException();
        }

        public override void SetUp()
        {
            throw new NotImplementedException();
        }
    }
}
