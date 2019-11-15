using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSharp.util
{
    class EntityStateMachine
    {
        public Dictionary<string, EntityState> states;

        public EntityStateMachine(Dictionary<string, EntityState> states)
        {
            this.states = new Dictionary<string, EntityState>();
        }

        public EntityState CreateState(string stateName)
        {
            EntityState es = new EntityState();
            s
        }
    }
}
