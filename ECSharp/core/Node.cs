using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSharp.core
{
    /// <summary>
    /// A node is a set of different of components.
    /// Nodes can be required by a system
    /// </summary>
    abstract class Node
    {

        public Entity entity;
        private string classId;

        public string ClassId { get => classId; set => classId = value; }

        protected Node(Entity entity, string classId)
        {
            this.entity = entity;
            this.ClassId = classId;
        }

        protected Node(Entity entity)
        {
            this.entity = entity;
            ClassId = GetType().Name;
        }

        /// <summary>
        /// This method should return a list of the component is acting on
        /// </summary>
        /// <returns>returns List<Component></returns>
        public abstract List<Component> getComponents();

        /// <summary>
        /// Derefence everything about the node
        /// </summary>
        public abstract void disposeNode();

        public abstract void setComponent(Component comp, string compId);
    }
}
