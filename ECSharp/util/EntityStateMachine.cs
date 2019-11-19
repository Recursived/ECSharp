using ECSharp.core;
using System;
using System.Collections.Generic;

namespace ECSharp.util
{
    public class EntityStateMachine
    {
        // TO-DO : changer la visibilité des champs en fonction de l'utilisation
        public Dictionary<string, EntityState> states;
        public Entity entity;


        public EntityStateMachine(Entity e)
        {
            states = new Dictionary<string, EntityState>();
            entity = e;
        }

        public EntityState CreateState(string stateName, bool discard)
        {
            EntityState es = new EntityState(stateName);
            es.discardable = discard;
            states.Add(stateName, es);
            return es;
        }
        /// <summary>
        /// Change the state of the fsm
        /// </summary>
        /// <param name="stateID">Name of the state</param>
        /// <param name="discard">If true, we discard components of the entity</param>
        public void ChangeState(string stateID)
        {

            ChangeEntityComponent(stateID);
        }


        private void ChangeEntityComponent(string stateID)
        {
            if (states.ContainsKey(stateID))
            {
                entity.RemoveComponents();
                EntityState es = states[stateID];
                foreach (Component c in es.GetComponents())
                {
                    entity.AddComponent(c);
                }
            }
            else
            {
                throw new Exception("The stateID : " + stateID + " does not correspond to any state on this machine");
            }
        }
    }
}
