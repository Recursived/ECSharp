using ECSharp.core;
using System.Drawing;

namespace SpaceInvaders.components
{
    class Display : Component
    {
        public Bitmap bitmap;
        public int rotation;

        public Display(int rotation, Bitmap b) : base()
        {
            this.rotation = rotation;
            bitmap = b;
        }

        public Display() : this(0, null) { }

        public Display(Bitmap b) : this(0, b) { }

        public Display(Display d) : this(d.rotation, d.bitmap) { }


        public override Component CreateCopy()
        {
            return new Display(this);
        }
    }
}
