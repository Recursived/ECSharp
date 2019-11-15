using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSharp.core
{
    /// <summary>
    /// The entity class is a placeholder of components
    /// It holds various reference of components that can be changed
    /// during the program 
    /// </summary>
    class Entity
    {
        private static int instanceCount = 0;
        internal Dictionary<string, Component> components;
        private string name;

        public Entity(string name)
        {
            this.components = new Dictionary<string, Component>();
            Name = name;
        }

        public Entity() : this("entity_"+ instanceCount++) { }

        public string Name { get => name; set => name = value; }
        

        public Component GetComponent(string compId)
        {
            return components[compId];
        }
        /// <summary>
        /// Add a component to a dictionary of components
        /// </summary>
        /// <param name="c">The component to add</param>
        public void AddComponent(Component c)
        {
            if (components.ContainsKey(c.ClassId)){
                components[c.ClassId] = c;
            } else
            {
                components.Add(c.ClassId, c);
            }
            
        }
        /// <summary>
        /// Remove a component from a dictionary
        /// </summary>
        /// <param name="c">The component to remove</param>
        /// <returns>If the component is in the dictionary, it is returned else returns null</returns>
        public Component RemoveComponent(Component c)
        {
            if (components.ContainsKey(c.ClassId))
            {
                Component comp = components[c.ClassId];
                components.Remove(c.ClassId);
                return comp;
            } else
            {
                return null;
            }
        }
        
        /// <summary>
        /// Remove every Components of the entity 
        /// </summary>
        public void RemoveComponents()
        {
            foreach (Component c in components.Values)
            {
                components.Remove(c.ClassId);
            }
        }
        /// <summary>
        /// Checks if the components is already in the entity
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Returns a bool indicating if the components already exists in the dictionary</returns>
        public bool HasComponents(Component c)
        {
            return components.ContainsKey(c.ClassId);
        }

        /// <summary>
        /// Get all the components of the entity
        /// </summary>
        /// <returns>Returns a list of all the components of the entity</returns>
        public List<Component> GetAllComponents()
        {
            return components.Values.ToList();
        }

    }
}
