using ECSharp.core;
using ECSharp.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class SpaceShip : Component
    {
        public int life;

        public SpaceShip(int life) : base()
        {
            this.life = life;
        }

        public SpaceShip() : this(0) {}

        public SpaceShip(SpaceShip s) : this(s.life) { }

        public override Component CreateCopy()
        {
            return new SpaceShip(this);
        }
    }
}
