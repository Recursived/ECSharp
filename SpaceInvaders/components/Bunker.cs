using ECSharp.core;

namespace SpaceInvaders.components
{
    class Bunker : Component
    {
        public int life;

        public Bunker(int life) : base()
        {
            this.life = life;
        }

        public Bunker() : this(0) { }

        public Bunker(Bunker b) : this(b.life) { }

        public override Component CreateCopy()
        {
            return new Bunker(this);
        }
    }
}
