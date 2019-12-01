using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.systems
{
    class AnimationNode : Node
    {
        public Animation anim;
        public Position pos;

        public AnimationNode() : base() { }

        public AnimationNode(Entity e) : base(e) { }

        public override void DisposeNode()
        {
            anim = null;
            pos = null;
        }

        public override List<Component> getComponents()
        {
            List<Component> lst = new List<Component>
            {
                anim,
                pos
            };
            return lst;
        }

        public override Node makeCopy(Entity e)
        {
            AnimationNode an = new AnimationNode(e);
            an.pos = pos;
            an.anim = anim;
            return an;
        }

        public override void SetComponent(Component comp)
        {
            if (comp.ClassId == anim.ClassId)
            {
                anim = (Animation)comp;
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
            pos = new Position();
            anim = new Animation(new util.BitmapAnimation());
        }
    }
}
