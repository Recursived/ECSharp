using ECSharp.core;
using SpaceInvaders.components;
using System.Collections.Generic;

namespace SpaceInvaders.nodes
{
    class GameStateNode : Node
    {
        public GameState gs;

        public GameStateNode() : base() { }
        public GameStateNode(Entity e) : base(e) { }


        public override void DisposeNode()
        {
            gs = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                gs
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            GameStateNode gsn = new GameStateNode();
            gsn.gs = gs;
            return gsn;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == gs.ClassId)
            {
                gs = (GameState)comp;
            }
        }

        public override void SetUp()
        {
            gs = new GameState(3);
        }
    }
}
