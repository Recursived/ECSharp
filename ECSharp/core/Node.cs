using System.Collections.Generic;

namespace ECSharp.core
{
    /// <summary>
    /// A node is a set of different of components.
    /// They should contain the components and entities they act on.
    /// They act as a sort of filter for components
    /// </summary>
    public abstract class Node
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

        protected Node() : this(null) { }

        /// <summary>
        /// This method should return a list of the component is acting on
        /// </summary>
        /// <returns>returns List<Component></returns>
        public abstract List<Component> getComponents();

        /// <summary>
        /// Derefence everything about the node
        /// </summary>
        public abstract void DisposeNode();

        /// <summary>
        /// Sets up the component of a node
        /// </summary>
        /// <param name="comp">Component</param>
        public abstract void SetComponent(Component comp);

        /// <summary>
        /// Sets up components of a node with dummy values
        /// </summary>
        public abstract void SetUp();

        /// <summary>
        /// Make a copy of the current node by passing reference of components
        /// </summary>
        /// <param name="e">Entity</param>
        /// <returns>Node</returns>
        public abstract Node makeCopy(Entity e);
    }
}
