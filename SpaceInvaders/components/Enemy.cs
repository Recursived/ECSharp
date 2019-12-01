using ECSharp.core;
using SpaceInvaders.util;

namespace SpaceInvaders.components
{
    class Enemy : Component
    {
        public int life;
        public enum Type
        {
            Small,
            Medium,
            Big,
        }

        public Type type;
        public Vector2D vitesse;

        public Enemy(int life, Type type, float vx, float vy) : base()
        {
            this.life = life;
            this.type = type;
            this.vitesse = new Vector2D(vx, vy);

        }

        public Enemy(int life, Type type, Vector2D v) : this(life, type, v.x, v.y) { }

        public Enemy(Type type) : this(0, type, 0, 0) { }

        public Enemy(Enemy e) : this(e.life, e.type, 0, 0) { }

        public override Component CreateCopy()
        {
            return new Enemy(this);
        }
    }
}
