using Magnum.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ECSharp.core
{
    /// <summary>
    /// The engine is the main part of the ECS system. It holds every system and entities.
    /// It also holds sets of nodes.
    /// </summary>
    public class Engine
    {
        #region fields
        private LinkedList<Entity> entityList;
        private OrderedBag<Systeme> systemList;
        private Dictionary<string, Entity> entityDict;
        private Dictionary<string, IGrouping> groupings;
        public bool updating;
        #endregion

        public Engine()
        {
            entityList = new LinkedList<Entity>();
            systemList = new OrderedBag<Systeme>();
            entityDict = new Dictionary<string, Entity>();
            groupings = new Dictionary<string, IGrouping>();

        }

        /// <summary>
        /// Adds an entity to the ECS model
        /// </summary>
        /// <param name="e">Entity</param>
        public void AddEntity(Entity e)
        {
            if (entityDict.ContainsKey(e.Name))
            {
                throw new Exception("Entity name : " + e.Name + " is already used !");
            }

            entityList.AddLast(e);
            entityDict[e.Name] = e;
            foreach (IGrouping g in groupings.Values)
            {
                g.NewEntity(e);
            }
        }

        /// <summary>
        /// Removes an entity from the ecs system
        /// </summary>
        /// <param name="e">Entity</param>
        public void RemoveEntity(Entity e)
        {
            foreach (IGrouping g in groupings.Values)
            {
                g.RemoveEntity(e);
            }
            entityDict.Remove(e.Name);
            entityList.Remove(e);
        }

        /// <summary>
        /// Get the entity according to the name given by parameter
        /// </summary>
        /// <param name="entityID">string entity id</param>
        /// <returns>Entity</returns>
        public Entity GetEntityByName(string entityID)
        {
            return entityDict[entityID];
        }

        public void RemoveAllEntities()
        {
            foreach (Entity e in entityList)
            {
                RemoveEntity(e);
            }
        }

        public LinkedList<Entity> GetEntities()
        {
            return entityList;
        }




        /// <summary>
        /// Get the list of node by checking if it is presents it the groupings. If not
        /// the node manager has to fetch the set of nodes
        /// </summary>
        /// <param name="node">Node : type of node</param>
        /// <returns>LinkedList<Node></returns>
        public LinkedList<Node> GetNodeList(Node node)
        {
            if (groupings.ContainsKey(node.ClassId))
            {
                return groupings[node.ClassId].GetNodes();
            }
            NodeManager group = new NodeManager(this, node);
            groupings[node.ClassId] = group;
            foreach (Entity e in entityList)
            {
                group.NewEntity(e);
            }

            return group.GetNodes();
        }

        public void ReleaseNodeList(Node n)
        {
            if (groupings.ContainsKey(n.ClassId))
            {
                groupings[n.ClassId].Clean();
            }
            groupings.Remove(n.ClassId);
        }


        public void AddSystem(Systeme s, Systeme.Priority p)
        {
            s.priority = p;
            s.AddIntoEngine(this);
            systemList.Add(s);
        }

        public Systeme GetSystem(string s)
        {
            foreach (Systeme sys in systemList)
            {
                if (sys.ClassId == s)
                {
                    return sys;
                }
            }
            return systemList.FindAll(x => x.GetType().Name == s).GetEnumerator().Current;
        }

        public OrderedBag<Systeme> GetSystems()
        {
            return systemList;
        }

        public void RemoveSystem(Systeme s)
        {
            systemList.Remove(s);
            s.RemoveFromEngine(this);
        }

        public void RemoveAllSystems()
        {
            foreach (Systeme s in systemList)
            {
                RemoveSystem(s);
            }
            systemList.Clear();
        }
        /// <summary>
        /// Updates every system in the engine
        /// </summary>
        /// <param name="time">float time</param>
        /// <param name="g">if Graphics is different of null, the update is more of a draw</param>
        public void Update(float time, Graphics g)
        {
            updating = true;
            foreach (Systeme s in systemList)
            {
                s.update(time, g);
            }

            updating = false;

        }
    }
}