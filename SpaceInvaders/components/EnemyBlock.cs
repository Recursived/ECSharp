using ECSharp.core;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class EnemyBlock : Component
    {
        public Vector2D upperLeft;
        public Vector2D bottomRight;
        public enum Direction
        {
            Left,
            Right
        }
        public Direction direction;
        public bool collided;
        public int shootProbability;

        public EnemyBlock(float p1x, float p1y, float p2x, float p2y) : base()
        {
            upperLeft = new Vector2D(p1x, p1y);
            bottomRight = new Vector2D(p2x, p2y);
            direction = Direction.Right;
            collided = false;
            shootProbability = 1000;
        }

        public EnemyBlock(Vector2D v1, Vector2D v2) : this(v1.x, v1.y, v2.x, v2.y) { }

        public EnemyBlock(EnemyBlock eb) : this(eb.upperLeft, eb.bottomRight) { }
        public override Component CreateCopy()
        {
            return new EnemyBlock(this);
        }
    }
}
