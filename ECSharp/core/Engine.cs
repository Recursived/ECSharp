using Magnum.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ECSharp.core
{
    public class Engine
    {
        private LinkedList<Entity> entityList;
        private OrderedBag<Systeme> systemList;
        private Dictionary<string, Entity> entityDict;
        private Dictionary<string, IGrouping> groupings;
        public bool updating;
        private NodeManager nodeManager;

        public Engine()
        {
            entityList = new LinkedList<Entity>();
            systemList = new OrderedBag<Systeme>();
            entityDict = new Dictionary<string, Entity>();
            groupings = new Dictionary<string, IGrouping>();

        }

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

        public void RemoveEntity(Entity e)
        {
            foreach (IGrouping g in groupings.Values)
            {
                g.RemoveEntity(e);
            }
            entityDict.Remove(e.Name);
            entityList.Remove(e);
        }

        private void EntityNameChanged(Entity e, string oldID)
        {
            if (entityDict.ContainsKey(oldID))
            {
                entityDict.Remove(oldID);
                entityDict.Add(e.Name, e);
            }
        }

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

        private void ComponentAdded(Entity e, string compId)
        {
            foreach (IGrouping g in groupings.Values)
            {
                g.ComponentAddedToEntity(e, compId);
            }
        }

        private void ComponentRemoved(Entity e, string compId)
        {
            foreach (IGrouping g in groupings.Values)
            {
                g.ComponentRemovedFromEntity(e, compId);
            }
        }

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

        public Systeme GetSystem(Systeme s)
        {
            return systemList.FindAll(x => x.GetType().Name.Equals(s.GetType().Name)).GetEnumerator().Current;
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