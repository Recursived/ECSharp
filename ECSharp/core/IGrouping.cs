using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSharp.core
{
    interface IGrouping
    {
        LinkedList<Node> GetNodes();
        void NewEntity(Entity e);
        void RemoveEntity(Entity e);
        void ComponentAddedToEntity(Entity e, string compId);
        void ComponentRemovedFromEntity(Entity e, string compId);
        void Clean();
    }
}
