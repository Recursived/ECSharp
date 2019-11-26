using ECSharp.core;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
