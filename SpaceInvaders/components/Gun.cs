using ECSharp.core;
using SpaceInvaders.util;

namespace SpaceInvaders.components
{
    class Gun : Component
    {
        public bool shoot;
        public Vector2D shootingPoint;
        public bool doubleShoot;
        public bool controllableShoot;

        public Gun(float x, float y) : base()
        {
            shootingPoint = new Vector2D(x, y);
            shoot = true;
            doubleShoot = false;
            controllableShoot = false;
        }
        public Gun(Vector2D v) : this(v.x, v.y) { }

        public Gun(Gun g) : this(g.shootingPoint.x, g.shootingPoint.y) { }

        public override Component CreateCopy()
        {
            return new Gun(this);
        }
    }
}
