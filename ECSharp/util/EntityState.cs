using ECSharp.core;
using System.Collections.Generic;

namespace ECSharp.util
{
    public class EntityState
    {
        private string stateID;
        List<Component> initComponents;
        public bool discardable = true;

        public EntityState(string stateID)
        {
            this.stateID = stateID;
            initComponents = new List<Component>();
        }
        public EntityState Add(Component c)
        {
            initComponents.Add(c);
            return this;
        }
        public List<Component> GetComponents()
        {
            if (discardable)
            {
                List<Component> lc = new List<Component>();
                foreach (Component c in initComponents)
                {
                    // We create a component with the init ref and inject it into the returned list
                    Component nc = c.CreateCopy();
                    lc.Add(nc);
                }
                return lc;
            }
            else
            {
                return initComponents;
            }

        }
    }
}
