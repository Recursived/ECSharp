using System.Collections.Generic;

namespace ECSharp.core
{
    /// <summary>
    /// The node manager is a class that groups entities and comps according to the type of the node it has.
    /// It checks the components in the nodes and filter entities according to those
    /// </summary>
    class NodeManager : IGrouping
    {
        private Engine engine;
        private LinkedList<Node> nodes;
        private Dictionary<string, Node> entities;
        private Dictionary<string, Component> components;
        //private NodeCache nodeCache;
        private Node nodeType;

        public NodeManager(Engine engine, Node nodeType)
        {
            Init(engine, nodeType);
        }

        /// <summary>
        /// Initialize the node manager
        /// </summary>
        /// <param name="engine">Engine</param>
        /// <param name="nodeType">Node</param>
        private void Init(Engine engine, Node nodeType)
        {
            this.engine = engine;
            this.nodeType = nodeType;
            nodes = new LinkedList<Node>();
            entities = new Dictionary<string, Node>();
            components = new Dictionary<string, Component>();

            //            nodeCache = new NodeCache(nodeType, components);

            foreach (Component c in nodeType.getComponents())
            {
                components.Add(c.ClassId, c);
            }
        }

        public LinkedList<Node> GetNodes()
        {
            return nodes;
        }

        public void NewEntity(Entity e)
        {
            AddIfMatch(e);
        }

        public void RemoveEntity(Entity e)
        {
            removeIfMatch(e);
        }

        public void ComponentAddedToEntity(Entity e, string compId)
        {
            AddIfMatch(e);
        }

        public void ComponentRemovedFromEntity(Entity e, string compId)
        {
            if (components.ContainsKey(compId))
            {
                removeIfMatch(e);
            }
        }




        /// <summary>
        /// Adds entity according to the components in them
        /// </summary>
        /// <param name="e">Entity</param>
        private void AddIfMatch(Entity e)
        {
            if (!entities.ContainsKey(e.Name))
            {
                foreach (Component comp in components.Values)
                {
                    if (!e.HasComponents(comp))
                    {
                        return;
                    }
                }

                Node n = nodeType.makeCopy(e);
                foreach (Component c in components.Values)
                {
                    n.SetComponent(e.GetComponent(c.ClassId));
                }

                entities[e.Name] = n;
                nodes.AddLast(n);

            }
        }

        private void removeIfMatch(Entity e)
        {
            if (entities.ContainsKey(e.Name))
            {
                Node n = entities[e.Name];
                entities.Remove(e.Name);
                nodes.Remove(n);
            }
        }

        /// <summary>
        /// Clear nodes and entites of the node manager
        /// </summary>
        public void Clean()
        {
            foreach (Node n in nodes)
            {
                entities.Remove(n.ClassId);
            }
            nodes.Clear();
        }

    }
}
