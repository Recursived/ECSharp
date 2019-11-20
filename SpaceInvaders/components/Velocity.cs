using ECSharp.core;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class Velocity : Component
    {
        public Vector2D speedVect;
        public int damplingScalar;

        public Velocity(float vx, float vy, int dampling) : base()
        {
            speedVect = new Vector2D(vx, vy);
            damplingScalar = dampling;

        }

        public Velocity() : this(0, 0, 0) { }

        public Velocity(Vector2D v, int dampling) : this(v.x, v.y, dampling) { }

        public Velocity(Velocity velocity) : this(velocity.speedVect.x, velocity.speedVect.y, velocity.damplingScalar) { }
        public override Component CreateCopy()
        {
            return new Velocity(this);
        }
    }
}
