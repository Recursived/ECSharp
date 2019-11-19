namespace ECSharp.core
{
    public abstract class Systeme
    {
        public enum Priority : int
        {
            None = -1;
            Update = 0,
            Move = 1,
            Collisions = 2,
            Animate = 3,
            Render = 4
        }

        internal Priority priority;

        protected Systeme(Priority priority)
        {
            this.priority = priority;
        }

        protected Systeme() : this(Priority.None) { }

        public abstract void AddIntoEngine(Engine e);

        public abstract void RemoveFromEngine(Engine e);

        public abstract void update(int time);
    }
}
