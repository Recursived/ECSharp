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
        public EntityStateMachine esm;

        public SpaceShip(int life, EntityStateMachine esm) : base()
        {
            this.life = life;
            this.esm = esm;
        }

        public SpaceShip(EntityStateMachine esm) : this(0, esm) {}

        public SpaceShip(SpaceShip s) : this(s.life, s.esm) { }

        public override Component CreateCopy()
        {
            return new SpaceShip(this);
        }
    }
}
