using ECSharp.core;
using ECSharp.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class Enemy : Component
    {
        public int life;
        public EntityStateMachine esm;
        public enum Type
        {
            Small,
            Medium,
            Big,
        }

        public Type type;

        public Enemy(int life, Type type, EntityStateMachine esm) : base()
        {
            this.life = life;
            this.type = type;
            this.esm = esm;
        }

        public Enemy(Type type, EntityStateMachine esm) : this(0, type, esm) { }

        public Enemy(Enemy e) : this(e.life, e.type, e.esm) { }

        public override Component CreateCopy()
        {
            return new Enemy(this);
        }
    }
}
