using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.util
{
    class Vector2D
    {
        public float x;
        public float y;
        private double norme;

        public double Norme { 
            get => norme;
            private set => norme = value;
        }

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
            Norme = Math.Sqrt((x * x) + (y * y));
        }

        public Vector2D() : this(0, 0) { }
        public static Vector2D operator+(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2D operator-(Vector2D v)
        {
            return new Vector2D(-v.x, -v.y);
        }

        public static Vector2D operator-(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2D operator*(float k, Vector2D v)
        {
            return new Vector2D(k * v.x, k * v.y);
        }

        public static Vector2D operator *(Vector2D v, float k)
        {
            return new Vector2D(k * v.x, k * v.y);
        }

        public static Vector2D operator/(Vector2D v, float k)
        {
            return new Vector2D(v.x / k, v.y * k);
        }

        public override string ToString()
        {
            return "Vector2D(" + x + ", " + y + ")";
        }


    }
}
