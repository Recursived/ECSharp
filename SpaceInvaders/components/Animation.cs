using ECSharp.core;
using SpaceInvaders.util;

namespace SpaceInvaders.components
{
    class Animation : Component
    {
        public BitmapAnimation bitmapanimation;

        public Animation(BitmapAnimation anim) : base()
        {
            bitmapanimation = anim;
        }

        public Animation(Animation a) : this(a.bitmapanimation) { }
        public override Component CreateCopy()
        {
            return new Animation(this);
        }
    }
}
