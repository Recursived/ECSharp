namespace ECSharp.core
{
    /// <summary>
    /// Base for components elements.
    /// Components subclasses are just data holding classes
    /// used by entities and nodes
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// string that should be unique have the name of the subclass.
        /// Used by the dictionnary of the entity class
        /// </summary>
        private string classId;

        protected Component(string classId)
        {
            this.classId = classId;
        }

        protected Component()
        {
            classId = GetType().Name;
        }

        protected Component(Component c)
        {
            classId = c.classId;
        }

        /// <summary>
        ///  Create a copy of a component
        /// </summary>
        /// <returns>Component</returns>
        public abstract Component CreateCopy();

        public string ClassId => this.classId;

    }
}
