using ECSharp.core;
using System.Windows.Forms;

namespace SpaceInvaders.components
{
    class GunControl : Component
    {
        public Keys space;

        public GunControl(Keys space) : base()
        {
            this.space = space;
        }

        public GunControl(GunControl gc) : this(gc.space) { }

        public override Component CreateCopy()
        {
            return new GunControl(this);
        }
    }
}
