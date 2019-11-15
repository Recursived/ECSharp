using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSharp.core
{
    /// <summary>
    /// Base for components elements.
    /// Components subclasses are just data holding classes
    /// used by entities and nodes
    /// </summary>
    abstract class Component
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

        protected Component() {
            classId = GetType().Name;
        }

        protected Component(Component c)
        {
            classId = c.classId;
        }

        public abstract Component CreateCopy();

        public string ClassId => this.classId;

    }
}
