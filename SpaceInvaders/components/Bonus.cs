using ECSharp.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.components
{
    class Bonus : Component
    {
        public enum Type
        {
            RestoreBunker,
            PlusLife,
            ScoreBoost,


        }

        public Bonus.Type type;

        public Bonus(Type type) : base()
        {
            this.type = type;
        }

        public Bonus() : this(Bonus.Type.PlusLife) { }

        public Bonus(Bonus b) : this(b.type) { }

        public override Component CreateCopy()
        {
            return new Bonus(this);
        }
    }
}
