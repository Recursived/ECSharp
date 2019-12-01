using ECSharp.core;
using System.Windows.Forms;

namespace SpaceInvaders.components
{
    class SpaceShipControl : Component
    {
        public Keys left;
        public Keys right;
        public Velocity v;
        public SpaceShipControl(Keys left, Keys right, Velocity v) : base()
        {
            this.left = left;
            this.right = right;
            this.v = v;
        }

        public SpaceShipControl(SpaceShipControl ssc) : this(ssc.left, ssc.right, ssc.v) { }

        public override Component CreateCopy()
        {
            return new SpaceShipControl(this);
        }
    }
}
