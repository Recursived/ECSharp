﻿using System;
using System.Drawing;

namespace ECSharp.core
{
    public abstract class Systeme : IComparable
    {
        public enum Priority : int
        {
            None = -1,
            Preupdate = 0,
            Update = 1,
            Move = 2,
            Collision = 3,
            Animate = 4,
            Render = 5
        }

        internal Priority priority;
        protected bool runnable;
        private string classId;

        

        protected Systeme(Priority priority)
        {
            this.priority = priority;
            runnable = false;
            classId = GetType().Name;
        }

        protected Systeme() : this(Priority.None) { }

        public void Start() { runnable = true; }

        public void Stop() { runnable = false; }

        public abstract void AddIntoEngine(Engine e);

        public abstract void RemoveFromEngine(Engine e);

        public abstract void update(float time, Graphics g);

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Systeme s = obj as Systeme;
            if (s != null)
                return this.priority.CompareTo(s.priority);
            else
                throw new ArgumentException("Object is not a Temperature");
        }

        public string ClassId => classId;
    }
}
