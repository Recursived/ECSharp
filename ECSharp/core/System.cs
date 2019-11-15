using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSharp.core
{
    abstract class System
    {
        public  enum Priority : int
        {
            Update = 0,
            Move = 1,
            Collisions = 2,
            Animate = 3,
            Render = 4
        }

        internal Priority priority;

        protected System(Priority priority)
        {
            this.priority = priority;
        }

        public abstract void AddIntoEngine(Engine e);

        public abstract void RemoveFromEngine(Engine e);

        public abstract void update(int time);
    }
}
