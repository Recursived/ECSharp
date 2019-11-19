using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECSharp.core;
using SpaceInvaders.util;

namespace SpaceInvaders.components
{
    class Position : Component
    {
        public Vector2D point;
        public int rotation;

        public Position(float x, float y, int rotation) : base()
        {
            this.point = new Vector2D(x, y);
            this.rotation = rotation;
        }

        public Position(float x, float y) : this(x, y, 0) { }

        public Position() : this(0, 0, 0) { }

        public Position(Vector2D v, int rotation) : this(v.x, v.y, rotation) { }

        public Position(Position p) : this(p.point.x, p.point.y, p.rotation) { }


        public override Component CreateCopy()
        {
            return new Position(this);
        }

    }
}
