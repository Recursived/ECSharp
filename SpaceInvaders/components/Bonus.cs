using ECSharp.core;

namespace SpaceInvaders.components
{
    class Bonus : Component
    {
        public enum Type
        {
            RestoreBunker,
            PlusLife,
            ScoreBoost,
            EnemySlow,
            doubleShoot,
            controlledBullet


        }

        public Type type;

        public Bonus(Type type) : base()
        {
            this.type = type;
        }

        public Bonus() : this(Type.PlusLife) { }

        public Bonus(Bonus b) : this(b.type) { }

        public override Component CreateCopy()
        {
            return new Bonus(this);
        }
    }
}
