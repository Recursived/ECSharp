using System.Collections.Generic;

namespace ECSharp.core
{
    /// <summary>
    /// Interface for classes that should deal with groupings (elements with same characteristics)
    /// </summary>
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
