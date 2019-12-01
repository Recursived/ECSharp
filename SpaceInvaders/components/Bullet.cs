using ECSharp.core;

namespace SpaceInvaders.components
{
    class Bullet : Component
    {
        public int damage;
        public bool alive;
        public bool ally;
        public bool controlled;

        public Bullet(int damage, bool ally, bool controlled = false) : base()
        {
            alive = false;
            this.ally = ally;
            this.damage = damage;
            this.controlled = controlled;
        }

        public Bullet(Bullet b) : this(b.damage, b.ally) { }

        public override Component CreateCopy()
        {
            return new Bullet(this);
        }
    }
}
