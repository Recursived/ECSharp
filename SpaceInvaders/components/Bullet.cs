using ECSharp.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class Bullet : Component
    {
        public int damage;
        public bool alive;

        public Bullet(int damage) : base()
        {
            alive = false;
            this.damage = damage;
        }

        public Bullet(Bullet b) : this(b.damage) { }

        public override Component CreateCopy()
        {
            return new Bullet(this);
        }
    }
}
