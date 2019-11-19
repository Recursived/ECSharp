using System;
using System.Collections.Generic;
using System.Linq;

namespace ECSharp.core
{
    class NodeCache
    {
        private Stack<Node> pool;
        private Node nodeType;
        private Stack<Node> cacheStack;
        private Dictionary<string, Component> components;

        public NodeCache(Node nodeType, Dictionary<string, Component> components)
        {
            this.nodeType = nodeType;
            this.components = components;
        }

        internal Node get()
        {
            try
            {
                return pool.Pop();
            }
            catch (Exception e)
            {
                return nodeType;
            }
        }

        internal void dispose(Node n)
        {
            n.disposeNode();
            pool.Push(n);
        }

        internal void cache(Node n)
        {
            cacheStack.Push(n);
        }

        internal void releaseCache()
        {
            for (int i = 0; i < cacheStack.Count(); i++)
            {
                dispose(cacheStack.Pop());
            }
        }
    }
}
