using ECSharp.core;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class Gun : Component
    {
        public bool shoot;
        public Vector2D shootingPoint;
        public float timeSinceLastShot;

        public Gun(float x, float y) : base()
        {
            shootingPoint = new Vector2D(x,y);
            shoot = true;
            timeSinceLastShot = 0;
        }
        public Gun(Vector2D v) : this(v.x, v.y) { }

        public Gun(Gun g) : this(g.shootingPoint.x, g.shootingPoint.y) { }

        public override Component CreateCopy()
        {
            return new Gun(this);
        }
    }
}
