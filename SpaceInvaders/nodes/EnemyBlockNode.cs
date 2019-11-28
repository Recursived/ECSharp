using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.nodes
{
    class EnemyBlockNode : Node
    {
        public EnemyBlock block;

        public EnemyBlockNode() : base() { }

        public EnemyBlockNode(Entity e) : base(e) { }
        public override void DisposeNode()
        {
            block = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                block
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            EnemyBlockNode eb = new EnemyBlockNode(e);
            eb.block = block;
            return eb;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == block.ClassId)
            {
                block = (EnemyBlock)comp;
            }
            else
            {
                throw new Exception("You passed a wrong component to the " + GetType().Name + " class");
            }
        }

        public override void SetUp()
        {
            block = new EnemyBlock(0, 0, 0, 0);
        }
    }
}
