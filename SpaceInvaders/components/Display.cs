using ECSharp.core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class Display : Component
    {
        public Bitmap bitmap;
        public float x;
        public float y;
        public int rotation;

        public Display(float x, float y, int rotation, Bitmap b) : base()
        {
            this.x = x;
            this.y = y;
            this.rotation = rotation;
            bitmap = b;
        }

        public Display(Bitmap b) : this(0, 0, 0, b) { }

        public Display(Display d) : this(d.x, d.y, d.rotation, d.bitmap) { }

        
        public override Component CreateCopy()
        {
            return new Display(this);
        }
    }
}
